namespace Com.Htc.VR.Core;

unsafe partial class EventLink
{
    ~EventLink()
    {
        _members.InstanceMethods.InvokeNonvirtualVoidMethod("finalize.()V", this, null);
    }
}
