namespace Com.Htc.VR.Core.Jni;

unsafe partial class LockFreeRingBuffer
{
    ~LockFreeRingBuffer()
    {
        _members.InstanceMethods.InvokeNonvirtualVoidMethod("finalize.()V", this, null);
    }
}
