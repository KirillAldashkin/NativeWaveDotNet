namespace Com.Htc.VR.Core.Jni;

unsafe partial class SharedBuffer
{
    ~SharedBuffer()
    {
        _members.InstanceMethods.InvokeNonvirtualVoidMethod("finalize.()V", this, null);
    }
}
