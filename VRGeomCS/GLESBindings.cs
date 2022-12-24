using System.Runtime.InteropServices;

namespace VRGeomCS;

public static class GLESBindings
{
    private const string Lib = "libGLESv3.so";

    [DllImport(Lib, EntryPoint = "glClearColor")]
    public static extern void ClearColor(float r, float g, float b, float a);

    [DllImport(Lib, EntryPoint = "glClear")]
    public static extern void Clear(BufferType buffer);

    [DllImport(Lib, EntryPoint = "glViewport")]
    public static extern void Viewport(int x, int y, uint width, uint height);

    [DllImport(Lib, EntryPoint = "glBindFramebuffer")]
    public static extern void GlBindFramebuffer(FramebufferTarget target, uint framebuffer);

    [DllImport(Lib, EntryPoint = "glInvalidateFramebuffer")]
    public static extern void InvalidateFramebuffer(FramebufferTarget target, uint len, ref FramebufferAttachment bufferStart);

    [DllImport(Lib, EntryPoint = "glGenFramebuffers")]
    public static extern void GenFramebuffers(uint count, ref uint bufferStart);

    [DllImport(Lib, EntryPoint = "glDeleteRenderbuffers")]
    public static extern void DeleteRenderbuffers(uint count, ref uint bufferStart);

    [DllImport(Lib, EntryPoint = "glGenRenderbuffers")]
    public static extern void GenRenderbuffers(uint count, ref uint bufferStart);

    [DllImport(Lib, EntryPoint = "glBindRenderbuffer")]
    public static extern void BindRenderbuffer(RenderbufferTarget target, uint renderbuffer);

    [DllImport(Lib, EntryPoint = "glRenderbufferStorage")]
    public static extern void RenderbufferStorage(RenderbufferTarget target, SizedInternalFormat internalFormat, uint width, uint height);

    [DllImport(Lib, EntryPoint = "glFramebufferRenderbuffer")]
    public static extern void FramebufferRenderbuffer(FramebufferTarget target, FramebufferAttachment attachment, RenderbufferTarget renderbuffertarget, uint renderbuffer);

    [DllImport(Lib, EntryPoint = "glFramebufferTexture2D")]
    public static extern void FramebufferTexture2D(FramebufferTarget target, FramebufferAttachment attachment, TextureTarget textarget, uint texture, int level);

    [DllImport(Lib, EntryPoint = "glDeleteFramebuffers")]
    public static extern void DeleteFramebuffers(uint count, ref uint bufferStart);
    
    [DllImport(Lib, EntryPoint = "glEnable")]
    public static extern void Enable(EnableCap cap);

    [DllImport(Lib, EntryPoint = "glDepthFunc")]
    public static extern void DepthFunc(DepthFunction func);

    [DllImport(Lib, EntryPoint = "glDepthMask")]
    public static extern void DepthMask([MarshalAs(UnmanagedType.U1)] bool flag);

    [DllImport(Lib, EntryPoint = "glGetError")]
    public static extern ErrorCode GetError();

    public static void ThrowIfError()
    {
        var err = GetError();
        if (err == ErrorCode.NoError) return;
        throw new Exception($"GL error {err}");
    }

    public enum BufferType : int
    {
        DepthBufferBit = 0x100,
        StencilBufferBit = 0x400,
        ColorBufferBit = 0x4000
    }

    public enum FramebufferTarget : int
    {
        ReadFramebuffer = 0x8CA8,
        DrawFramebuffer = 0x8CA9,
        Framebuffer = 0x8D40
    }

    public enum FramebufferAttachment : int
    {
        DepthStencilAttachment = 0x821A,
        ColorAttachment0 = 0x8CE0,
        ColorAttachment1 = 0x8CE1,
        ColorAttachment2 = 0x8CE2,
        ColorAttachment3 = 0x8CE3,
        ColorAttachment4 = 0x8CE4,
        ColorAttachment5 = 0x8CE5,
        ColorAttachment6 = 0x8CE6,
        ColorAttachment7 = 0x8CE7,
        ColorAttachment8 = 0x8CE8,
        ColorAttachment9 = 0x8CE9,
        ColorAttachment10 = 0x8CEA,
        ColorAttachment11 = 0x8CEB,
        ColorAttachment12 = 0x8CEC,
        ColorAttachment13 = 0x8CED,
        ColorAttachment14 = 0x8CEE,
        ColorAttachment15 = 0x8CEF,
        ColorAttachment16 = 0x8CF0,
        ColorAttachment17 = 0x8CF1,
        ColorAttachment18 = 0x8CF2,
        ColorAttachment19 = 0x8CF3,
        ColorAttachment20 = 0x8CF4,
        ColorAttachment21 = 0x8CF5,
        ColorAttachment22 = 0x8CF6,
        ColorAttachment23 = 0x8CF7,
        ColorAttachment24 = 0x8CF8,
        ColorAttachment25 = 0x8CF9,
        ColorAttachment26 = 0x8CFA,
        ColorAttachment27 = 0x8CFB,
        ColorAttachment28 = 0x8CFC,
        ColorAttachment29 = 0x8CFD,
        ColorAttachment30 = 0x8CFE,
        ColorAttachment31 = 0x8CFF,
        DepthAttachment = 0x8D00,
        StencilAttachment = 0x8D20,
        ShadingRateAttachmentExt = 0x96D1
    }

    public enum SizedInternalFormat : int
    {
        DepthComponent24 = 0x81A6
    }

    public enum RenderbufferTarget : int
    {
        Renderbuffer = 0x8D41
    }

    public enum TextureTarget : int
    {
        Texture1D = 0xDE0,
        Texture2D = 0xDE1,
        ProxyTexture1D = 0x8063,
        ProxyTexture1DExt = 0x8063,
        ProxyTexture2D = 0x8064,
        ProxyTexture2DExt = 0x8064,
        Texture3D = 0x806F,
        Texture3DExt = 0x806F,
        Texture3DOes = 0x806F,
        ProxyTexture3D = 0x8070,
        ProxyTexture3DExt = 0x8070,
        DetailTexture2DSgis = 0x8095,
        Texture4DSgis = 0x8134,
        ProxyTexture4DSgis = 0x8135,
        TextureRectangle = 0x84F5,
        TextureRectangleArb = 0x84F5,
        TextureRectangleNV = 0x84F5,
        ProxyTextureRectangle = 0x84F7,
        ProxyTextureRectangleArb = 0x84F7,
        ProxyTextureRectangleNV = 0x84F7,
        TextureCubeMap = 0x8513,
        TextureCubeMapArb = 0x8513,
        TextureCubeMapExt = 0x8513,
        TextureCubeMapOes = 0x8513,
        TextureCubeMapPositiveX = 0x8515,
        TextureCubeMapPositiveXArb = 0x8515,
        TextureCubeMapPositiveXExt = 0x8515,
        TextureCubeMapPositiveXOes = 0x8515,
        TextureCubeMapNegativeX = 0x8516,
        TextureCubeMapNegativeXArb = 0x8516,
        TextureCubeMapNegativeXExt = 0x8516,
        TextureCubeMapNegativeXOes = 0x8516,
        TextureCubeMapPositiveY = 0x8517,
        TextureCubeMapPositiveYArb = 0x8517,
        TextureCubeMapPositiveYExt = 0x8517,
        TextureCubeMapPositiveYOes = 0x8517,
        TextureCubeMapNegativeY = 0x8518,
        TextureCubeMapNegativeYArb = 0x8518,
        TextureCubeMapNegativeYExt = 0x8518,
        TextureCubeMapNegativeYOes = 0x8518,
        TextureCubeMapPositiveZ = 0x8519,
        TextureCubeMapPositiveZArb = 0x8519,
        TextureCubeMapPositiveZExt = 0x8519,
        TextureCubeMapPositiveZOes = 0x8519,
        TextureCubeMapNegativeZ = 0x851A,
        TextureCubeMapNegativeZArb = 0x851A,
        TextureCubeMapNegativeZExt = 0x851A,
        TextureCubeMapNegativeZOes = 0x851A,
        ProxyTextureCubeMap = 0x851B,
        ProxyTextureCubeMapArb = 0x851B,
        ProxyTextureCubeMapExt = 0x851B,
        Texture1DArray = 0x8C18,
        ProxyTexture1DArray = 0x8C19,
        ProxyTexture1DArrayExt = 0x8C19,
        Texture2DArray = 0x8C1A,
        ProxyTexture2DArray = 0x8C1B,
        ProxyTexture2DArrayExt = 0x8C1B,
        TextureBuffer = 0x8C2A,
        Renderbuffer = 0x8D41,
        TextureCubeMapArray = 0x9009,
        TextureCubeMapArrayArb = 0x9009,
        TextureCubeMapArrayExt = 0x9009,
        TextureCubeMapArrayOes = 0x9009,
        ProxyTextureCubeMapArray = 0x900B,
        ProxyTextureCubeMapArrayArb = 0x900B,
        Texture2DMultisample = 0x9100,
        ProxyTexture2DMultisample = 0x9101,
        Texture2DMultisampleArray = 0x9102,
        ProxyTexture2DMultisampleArray = 0x9103
    }

    public enum EnableCap : int
    {
        PointSmooth = 0xB10,
        LineSmooth = 0xB20,
        LineStipple = 0xB24,
        PolygonSmooth = 0xB41,
        PolygonStipple = 0xB42,
        CullFace = 0xB44,
        Lighting = 0xB50,
        ColorMaterial = 0xB57,
        Fog = 0xB60,
        DepthTest = 0xB71,
        StencilTest = 0xB90,
        Normalize = 0xBA1,
        AlphaTest = 0xBC0,
        Dither = 0xBD0,
        Blend = 0xBE2,
        IndexLogicOp = 0xBF1,
        ColorLogicOp = 0xBF2,
        ScissorTest = 0xC11,
        TextureGenS = 0xC60,
        TextureGenT = 0xC61,
        TextureGenR = 0xC62,
        TextureGenQ = 0xC63,
        AutoNormal = 0xD80,
        Map1Color4 = 0xD90,
        Map1Index = 0xD91,
        Map1Normal = 0xD92,
        Map1TextureCoord1 = 0xD93,
        Map1TextureCoord2 = 0xD94,
        Map1TextureCoord3 = 0xD95,
        Map1TextureCoord4 = 0xD96,
        Map1Vertex3 = 0xD97,
        Map1Vertex4 = 0xD98,
        Map2Color4 = 0xDB0,
        Map2Index = 0xDB1,
        Map2Normal = 0xDB2,
        Map2TextureCoord1 = 0xDB3,
        Map2TextureCoord2 = 0xDB4,
        Map2TextureCoord3 = 0xDB5,
        Map2TextureCoord4 = 0xDB6,
        Map2Vertex3 = 0xDB7,
        Map2Vertex4 = 0xDB8,
        Texture1D = 0xDE0,
        Texture2D = 0xDE1,
        PolygonOffsetPoint = 0x2A01,
        PolygonOffsetLine = 0x2A02,
        ClipPlane0 = 0x3000,
        ClipDistance0 = 0x3000,
        ClipPlane1 = 0x3001,
        ClipDistance1 = 0x3001,
        ClipPlane2 = 0x3002,
        ClipDistance2 = 0x3002,
        ClipPlane3 = 0x3003,
        ClipDistance3 = 0x3003,
        ClipPlane4 = 0x3004,
        ClipDistance4 = 0x3004,
        ClipPlane5 = 0x3005,
        ClipDistance5 = 0x3005,
        ClipDistance6 = 0x3006,
        ClipDistance7 = 0x3007,
        Light0 = 0x4000,
        Light1 = 0x4001,
        Light2 = 0x4002,
        Light3 = 0x4003,
        Light4 = 0x4004,
        Light5 = 0x4005,
        Light6 = 0x4006,
        Light7 = 0x4007,
        Convolution1DExt = 0x8010,
        Convolution2DExt = 0x8011,
        Separable2DExt = 0x8012,
        HistogramExt = 0x8024,
        MinmaxExt = 0x802E,
        PolygonOffsetFill = 0x8037,
        RescaleNormalExt = 0x803A,
        Texture3DExt = 0x806F,
        VertexArray = 0x8074,
        NormalArray = 0x8075,
        ColorArray = 0x8076,
        IndexArray = 0x8077,
        TextureCoordArray = 0x8078,
        EdgeFlagArray = 0x8079,
        InterlaceSgix = 0x8094,
        Multisample = 0x809D,
        MultisampleSgis = 0x809D,
        SampleAlphaToCoverage = 0x809E,
        SampleAlphaToMaskSgis = 0x809E,
        SampleAlphaToOne = 0x809F,
        SampleAlphaToOneSgis = 0x809F,
        SampleCoverage = 0x80A0,
        SampleMaskSgis = 0x80A0,
        TextureColorTableSgi = 0x80BC,
        ColorTable = 0x80D0,
        ColorTableSgi = 0x80D0,
        PostConvolutionColorTable = 0x80D1,
        PostConvolutionColorTableSgi = 0x80D1,
        PostColorMatrixColorTable = 0x80D2,
        PostColorMatrixColorTableSgi = 0x80D2,
        Texture4DSgis = 0x8134,
        PixelTexGenSgix = 0x8139,
        SpriteSgix = 0x8148,
        ReferencePlaneSgix = 0x817D,
        IRInstrument1Sgix = 0x817F,
        CalligraphicFragmentSgix = 0x8183,
        FramezoomSgix = 0x818B,
        FogOffsetSgix = 0x8198,
        SharedTexturePaletteExt = 0x81FB,
        DebugOutputSynchronous = 0x8242,
        AsyncHistogramSgix = 0x832C,
        PixelTextureSgis = 0x8353,
        AsyncTexImageSgix = 0x835C,
        AsyncDrawPixelsSgix = 0x835D,
        AsyncReadPixelsSgix = 0x835E,
        FragmentLightingSgix = 0x8400,
        FragmentColorMaterialSgix = 0x8401,
        FragmentLight0Sgix = 0x840C,
        FragmentLight1Sgix = 0x840D,
        FragmentLight2Sgix = 0x840E,
        FragmentLight3Sgix = 0x840F,
        FragmentLight4Sgix = 0x8410,
        FragmentLight5Sgix = 0x8411,
        FragmentLight6Sgix = 0x8412,
        FragmentLight7Sgix = 0x8413,
        TextureRectangle = 0x84F5,
        TextureRectangleArb = 0x84F5,
        TextureRectangleNV = 0x84F5,
        TextureCubeMap = 0x8513,
        TextureCubeMapArb = 0x8513,
        TextureCubeMapExt = 0x8513,
        TextureCubeMapOes = 0x8513,
        ProgramPointSize = 0x8642,
        DepthClamp = 0x864F,
        TextureCubeMapSeamless = 0x884F,
        SampleShading = 0x8C36,
        RasterizerDiscard = 0x8C89,
        TextureGenStrOes = 0x8D60,
        PrimitiveRestartFixedIndex = 0x8D69,
        FramebufferSrgb = 0x8DB9,
        SampleMask = 0x8E51,
        FetchPerSampleArm = 0x8F65,
        PrimitiveRestart = 0x8F9D,
        DebugOutput = 0x92E0,
        ShadingRateImagePerPrimitiveNV = 0x95B1,
        FramebufferFetchNoncoherentQCom = 0x96A2,
        ShadingRatePreserveAspectRatioQCom = 0x96A5
    }

    public enum DepthFunction : int
    {
        Never = 0x200,
        Less = 0x201,
        Equal = 0x202,
        Lequal = 0x203,
        Greater = 0x204,
        Notequal = 0x205,
        Gequal = 0x206,
        Always = 0x207
    }

    public enum ErrorCode : int
    {
        NoError = 0x0,
        InvalidEnum = 0x500,
        InvalidValue = 0x501,
        InvalidOperation = 0x502,
        StackOverflow = 0x503,
        StackUnderflow = 0x504,
        OutOfMemory = 0x505,
        InvalidFramebufferOperation = 0x506,
        InvalidFramebufferOperationExt = 0x506,
        InvalidFramebufferOperationOes = 0x506,
        TableTooLargeExt = 0x8031,
        TableTooLarge = 0x8031,
        TextureTooLargeExt = 0x8065,
    }
}