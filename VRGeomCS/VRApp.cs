using GL = VRGeomCS.GLESBindings;

using Com.Htc.VR.BindingsCS;
using System.Runtime.CompilerServices;

namespace VRGeomCS;

public class VRApp
{
    public bool Running { get; set; } = false;

    private int width, height;
    private WVR.TextureQueueHandle leftEyeQ, rightEyeQ;
    private List<FrameBufferObject> leftFBOs = new(), rightFBOs = new();

    #region Init & Shutdown
    internal bool InitVR()
    {
        // Initialize runtime
        var initErr = WVR.Init(WVR.AppType.VRContent);
        if (initErr != WVR.InitError.None) return false;

        // Add listeners
        Span<WVR.InputAttribute> array = stackalloc WVR.InputAttribute[] {
            new()
            {
                ID = WVR.InputID.Menu,
                Capability = WVR.InputType.Button,
                AxisType = WVR.AnalogType.None
            },
            new()
            {
                ID = WVR.InputID.Grip,
                Capability = WVR.InputType.Button,
                AxisType = WVR.AnalogType.None
            },
            new()
            {
                ID = WVR.InputID.Touchpad,
                Capability = WVR.InputType.Button | WVR.InputType.Analog,
                AxisType = WVR.AnalogType.D2D
            },
            new()
            {
                ID = WVR.InputID.Trigger,
                Capability = WVR.InputType.Button | WVR.InputType.Analog,
                AxisType = WVR.AnalogType.D1D
            }
        };

        WVR.SetInputRequest(WVR.DeviceType.HMD, ref array[0], array.Length);
        WVR.SetInputRequest(WVR.DeviceType.ControllerRight, ref array[0], array.Length);
        WVR.SetInputRequest(WVR.DeviceType.ControllerLeft, ref array[0], array.Length);

        // Initialize render
        var renderInitParams = new WVR.RenderInitParams()
        {
            GraphicsApi = WVR.GraphicsApiType.OpenGL,
            RenderConfig = WVR.RenderConfig.Default
        };

        var renderInitErr = WVR.RenderInit(ref renderInitParams);
        if (renderInitErr != WVR.RenderError.None) return false;

        return true;
    }

    internal bool InitGL()
    {
        GL.Enable(GL.EnableCap.DepthTest);
        GL.DepthFunc(GL.DepthFunction.Less | GL.DepthFunction.Equal);
        GL.DepthMask(true);

        WVR.GetRenderTargetSize(out width, out height);
        if (width == 0 || height == 0) return false;

        leftEyeQ = WVR.ObtainTextureQueue(WVR.TextureTarget.D2D, WVR.TextureFormat.RGBA, WVR.TextureType.UnsignedByte, (uint)width, (uint)height, 0);
        if (!ProcessEye(leftEyeQ, leftFBOs)) return false;

        rightEyeQ = WVR.ObtainTextureQueue(WVR.TextureTarget.D2D, WVR.TextureFormat.RGBA, WVR.TextureType.UnsignedByte, (uint)width, (uint)height, 0);
        if (!ProcessEye(rightEyeQ, rightFBOs)) return false;

        return true;

        bool ProcessEye(WVR.TextureQueueHandle eye, List<FrameBufferObject> to)
        {
            var len = WVR.GetTextureQueueLength(eye);
            for (int i = 0; i < len; i++)
            {
                var fbo = FrameBufferObject.TryCreate((uint)WVR.GetTexture(eye, i).Id, (uint)width, (uint)height);
                if (fbo is null) return false;
                to.Add(fbo); 
            }
            return true;
        }
    }

    internal void ShutdownGL()
    {
        foreach (var fbo in leftFBOs) fbo.Clear();
        foreach (var fbo in rightFBOs) fbo.Clear();
        WVR.ReleaseTextureQueue(leftEyeQ);
        WVR.ReleaseTextureQueue(rightEyeQ);
    }

    internal void ShutdownVR() => WVR.Quit();
    #endregion

    internal void HandleInput()
    {

    }

    internal bool RenderFrame()
    {
        var indexLeft = WVR.GetAvailableTextureIndex(leftEyeQ);
        var indexRight = WVR.GetAvailableTextureIndex(rightEyeQ);

        RenderStereoTargets(indexLeft, indexRight);

        // Left
        var leftTexture = WVR.GetTexture(leftEyeQ, indexLeft);
        leftTexture.Layout.LeftLow = new(0, 0);
        leftTexture.Layout.RightUp = new(1, 1);
        var leftErr = WVR.SubmitFrame(WVR.Eye.Left, ref leftTexture, ref Unsafe.NullRef<WVR.PoseState>(), WVR.SubmitExtend.Default);
        if(leftErr != WVR.SubmitError.None) throw new($"Left eye submit error: {leftErr}");

        // Right 
        var rightTexture = WVR.GetTexture(rightEyeQ, indexRight);
        rightTexture.Layout.LeftLow = new(0, 0);
        rightTexture.Layout.RightUp = new(1, 1);
        var rightErr = WVR.SubmitFrame(WVR.Eye.Right, ref rightTexture, ref Unsafe.NullRef<WVR.PoseState>(), WVR.SubmitExtend.Default);
        if (rightErr != WVR.SubmitError.None) throw new($"Right eye submit error: {rightErr}");

        return true;
    }

    private void RenderStereoTargets(int indexLeft, int indexRight)
    {
        FrameBufferObject fbo;

        // Left
        fbo = leftFBOs[indexLeft];
        fbo.Bind();

        var leftTexture = WVR.GetTexture(leftEyeQ, indexLeft);
        fbo.FullViewport();
        WVR.PreRenderEye(WVR.Eye.Left, ref leftTexture, ref Unsafe.NullRef<WVR.RenderFoveationParams>());
        GL.Clear(GL.BufferType.DepthBufferBit | GL.BufferType.ColorBufferBit);
        RenderScene(WVR.Eye.Left);
        fbo.Unbind();

        // Right
        fbo = rightFBOs[indexRight];
        fbo.Bind();

        var rightTexture = WVR.GetTexture(rightEyeQ, indexRight);
        fbo.FullViewport();
        WVR.PreRenderEye(WVR.Eye.Right, ref rightTexture, ref Unsafe.NullRef<WVR.RenderFoveationParams>());
        GL.Clear(GL.BufferType.DepthBufferBit | GL.BufferType.ColorBufferBit);
        RenderScene(WVR.Eye.Right);
        fbo.Unbind();
    }

    private void RenderScene(WVR.Eye eye)
    {
        WVR.RenderMask(eye);
        
        if(eye == WVR.Eye.Left)
            GL.ClearColor(1, 0, 0, 1);
        else
            GL.ClearColor(0, 0, 1, 1);

        GL.Clear(GL.BufferType.DepthBufferBit | GL.BufferType.ColorBufferBit);
    }
}