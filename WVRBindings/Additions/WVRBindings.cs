using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Com.Htc.VR.BindingsCS;

public static unsafe class WVR
{
    private const string lib = "libwvr_api";

    [DllImport(lib, EntryPoint = "WVR_RegisterMain")]
    public static extern void RegisterMain(delegate* unmanaged[Cdecl]<int, sbyte**, int> mainFunc);

    [DllImport(lib, EntryPoint = "WVR_Init")]
    public static extern InitError Init(AppType type = AppType.VRContent);

    [DllImport(lib, EntryPoint = "WVR_Quit")]
    public static extern void Quit();

    [DllImport(lib, EntryPoint = "WVR_RenderInit")]
    public static extern RenderError RenderInit(ref RenderInitParams param);

    [DllImport(lib, EntryPoint = "WVR_SetInputRequest")]
    public static extern bool SetInputRequest(DeviceType type, ref InputAttribute first, int size);

    [DllImport(lib, EntryPoint = "WVR_GetRenderTargetSize")]
    public static extern void GetRenderTargetSize(out int width, out int height);

    [DllImport(lib, EntryPoint = "WVR_ObtainTextureQueue")]
    public static extern TextureQueueHandle ObtainTextureQueue(TextureTarget target, TextureFormat format, TextureType type, uint width, uint height, int level);

    [DllImport(lib, EntryPoint = "WVR_GetTextureQueueLength")]
    public static extern uint GetTextureQueueLength(TextureQueueHandle handle);

    [DllImport(lib, EntryPoint = "WVR_GetTexture")]
    public static extern TextureParams GetTexture(TextureQueueHandle handle, int index);

    [DllImport(lib, EntryPoint = "WVR_ReleaseTextureQueue")]
    public static extern void ReleaseTextureQueue(TextureQueueHandle handle);

    [DllImport(lib, EntryPoint = "WVR_GetAvailableTextureIndex")]
    public static extern int GetAvailableTextureIndex(TextureQueueHandle handle);

    [DllImport(lib, EntryPoint = "WVR_PreRenderEye")]
    public static extern void PreRenderEye(Eye eye, ref TextureParams textureParam, ref RenderFoveationParams foveatedParam);

    [DllImport(lib, EntryPoint = "WVR_SubmitFrame")]
    public static extern SubmitError SubmitFrame(Eye eye, ref TextureParams param, ref PoseState pose, SubmitExtend extendMethod);

    [DllImport(lib, EntryPoint = "WVR_RenderMask")]
    public static extern void RenderMask(Eye eye, TextureTarget target = TextureTarget.D2D);

    public enum InitError
    {
        None = 0,
        Unknown = 1,
        NotInitialized = 2
    }

    public enum AppType
    {
        VRContent = 1,
        ARContent = 2
    }

    public enum GraphicsApiType
    {
        OpenGL = 1
    }

    public enum RenderConfig : ulong
    {
        Default = 0,
        DisableSingleBuffer = 1,
        DisableReprojection = 2,
        sRGB = 4,
        GLNoError = 8,
        InitializeFadeOut = 32,
        InitializeUMC = 64,
        InitializePMC = 128,
        InitializeFrameSharpnessEnhancement = 256
    }

    public enum RenderError
    {
        None = 0,
        RuntimeInitFailed = 410,
        ContextSetupFailed = 411,
        DisplaySetupFailed = 412,
        LibNotSupported = 413,
        NullPtr = 414,
        Max = 65535,
    }

    [StructLayout(LayoutKind.Sequential, Pack = 0)]
    public struct RenderInitParams
    {
        public GraphicsApiType GraphicsApi;
        public RenderConfig RenderConfig;
        public GraphicsParams GraphicsParams;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 0)]
    public struct GraphicsParams
    {
        public IntPtr RenderContext;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 0)]
    public struct InputAttribute
    {
        public InputID ID;
        public InputType Capability;
        public AnalogType AxisType;
    }

    public enum InputID {
        Id0 = 0,
        Id1 = 1,
        Id2 = 2,
        Id3 = 3,
        Id4 = 4,
        Id5 = 5,
        Id6 = 6,
        Id7 = 7,
        Id8 = 8,
        Id9 = 9,
        Id10 = 10,
        Id11 = 11,
        Id12 = 12,
        Id13 = 13,
        Id14 = 14,
        Id15 = 15,
        Id16 = 16,
        Id17 = 17,
        Id18 = 18,
        Id19 = 19,

        System = Id0,
        Menu = Id1,
        Grip = Id2,
        DPadLeft = Id3,
        DPadUp = Id4,
        DPadRight = Id5,
        DPadDown = Id6,
        VolumeUp = Id7,
        VolumeDown = Id8,
        Bumper = Id9,
        A = Id10,
        B = Id11,
        X = Id12,
        Y = Id13,
        Back = Id14,
        Enter = Id15,
        Touchpad = Id16,
        Trigger = Id17,
        Thumbstick = Id18,
        Parking = Id19,

        Max = 32
    }

    public enum AnalogType {
        None = 0,
        D2D = 1,
        D1D = 2,
    }

    public enum InputType
    {
        Button = 1,
        Touch = 2,
        Analog = 4,
    }

    public enum DeviceType
    {
        Invalid = 0,
        HMD = 1,
        ControllerRight = 2,
        ControllerLeft = 3,
        Camera = 4,
        EyeTracking = 5,
        HandGestureRight = 6,
        HandGestureLeft = 7,
        NaturalHandRight = 8,
        NaturalHandLeft = 9,
        ElectronicHandRight = 10,
        ElectronicHandLeft = 11,
        Tracker = 12,
        Lip = 13,
        EyeExpression = 14
    }

    public enum TextureTarget
    {
        D2D,
        Array2D,
        External2D,
        DUAL2D,
        ExtDual2D,
        Vulkan
    }

    public enum TextureFormat
    {
        RGBA
    }

    public enum TextureType
    {
        UnsignedByte
    }

    public enum Eye
    {
        Left = 0,
        Right = 1,
        Both = 2,
    };

    public enum PeripheralQuality
    {
        Low = 0x0000,
        Medium = 0x0001,
        High = 0x0002
    }

    public enum SubmitError
    {
        None = 0,
        InvalidTexture = 400,
        ThreadStop = 401,
        BufferSubmitFailed = 402,
        Max = 65535
    }

    public enum SubmitExtend
    {
        Default = 0x0000,
        DisableDistortion = 0x0001,
        PartialTexture = 0x0010,
        SystemReserved1 = 1 << 30
    }

    public enum PoseOriginModel
    {
        OriginOnHead = 0,
        OriginOnGround = 1,
        OriginOnTrackingObserver = 2,
        OriginOnHead_3DoF = 3
    }

    [StructLayout(LayoutKind.Sequential, Pack = 0)]
    public struct TextureQueueHandle
    {
        public IntPtr Handle;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 0)]
    public struct TextureParams
    {
        public IntPtr Id;
        public TextureTarget Target;
        public TextureLayout Layout;
        public IntPtr Depth;
        private Matrix4x4* _projectionMatrix;

        public ref Matrix4x4 ProjectionMatrix => ref Unsafe.AsRef<Matrix4x4>(_projectionMatrix);
    }

    [StructLayout(LayoutKind.Sequential, Pack = 0)]
    public struct TextureLayout
    {
        public Vector2 LeftLow;
        public Vector2 RightUp;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 0)]
    public struct RenderFoveationParams
    {
        public float FocalX;
        public float FocalY;
        public float FovealFov;
        public PeripheralQuality PeriQuality;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 0)]
    public struct PoseState
    {
        public bool IsValid;
        public Matrix4x4 PoseMatrix;
        public Vector3 Velocity;
        public Vector3 AngularVelocity;
        public bool Is6DoFPose;
        public long Timestamp;
        public Vector3 Acceleration;
        public Vector3 AngularAcceleration;
        public float PredictedMilliSec;
        public PoseOriginModel OriginModel;
        public Pose Raw;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 0)]
    public struct Pose
    {
        public Vector3 Position;
        public Quaternion Rotation;
    }
}