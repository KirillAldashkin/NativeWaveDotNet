using GL = VRGeomCS.GLESBindings;

namespace VRGeomCS;

public class FrameBufferObject
{
    public uint Width { get; private set; }
    public uint Height { get; private set; }
    public uint TextureID { get; private set; }
    public uint FrameBufferID { get; private set; }
    public uint DepthBufferID { get; private set; }


    public void Clear()
    {
        if (DepthBufferID != 0)
        {
            var id = DepthBufferID;
            GL.DeleteRenderbuffers(1, ref id);
        }
        if (FrameBufferID != 0)
        {
            var id = FrameBufferID;
            GL.DeleteFramebuffers(1, ref id);
        }
        DepthBufferID = FrameBufferID = TextureID = 0;
    }

    public void Bind() => GL.GlBindFramebuffer(GL.FramebufferTarget.Framebuffer, FrameBufferID);

    public void Unbind()
    {
        Span<GL.FramebufferAttachment> data = stackalloc GL.FramebufferAttachment[]
        {
            GL.FramebufferAttachment.DepthAttachment, 
            GL.FramebufferAttachment.StencilAttachment
        };
        GL.InvalidateFramebuffer(GL.FramebufferTarget.Framebuffer, 2, ref data[0]);

        GL.GlBindFramebuffer(GL.FramebufferTarget.Framebuffer, 0);
    }

    public void FullViewport() => GL.Viewport(0, 0, Width, Height);

    public static FrameBufferObject? TryCreate(uint textureId, uint width, uint height)
    {
        uint frameBufferID = 0;
        GL.GenFramebuffers(1, ref frameBufferID);
        GL.GlBindFramebuffer(GL.FramebufferTarget.Framebuffer, frameBufferID);

        uint depthBufferID = 0;
        GL.GenRenderbuffers(1, ref depthBufferID);
        GL.BindRenderbuffer(GL.RenderbufferTarget.Renderbuffer, depthBufferID);
        GL.RenderbufferStorage(GL.RenderbufferTarget.Renderbuffer, GL.SizedInternalFormat.DepthComponent24, width, height);
        GL.FramebufferRenderbuffer(GL.FramebufferTarget.Framebuffer, GL.FramebufferAttachment.DepthAttachment, GL.RenderbufferTarget.Renderbuffer, depthBufferID);

        GL.FramebufferTexture2D(GL.FramebufferTarget.Framebuffer, GL.FramebufferAttachment.ColorAttachment0, GL.TextureTarget.Texture2D, textureId, 0);

        return new()
        {
            Width = width,
            Height = height,
            TextureID = textureId,
            DepthBufferID = depthBufferID,
            FrameBufferID = frameBufferID
        };
    }
} 