namespace Com.Htc.VR.Core.Jni;

unsafe partial class LockFreeSingleBuffer
{
    ~LockFreeSingleBuffer()
    {
        _members.InstanceMethods.InvokeNonvirtualVoidMethod("finalize.()V", this, null);
    }
}
