namespace Com.Htc.VR.Core;

unsafe partial class StateLink
{
    ~StateLink()
    {
        _members.InstanceMethods.InvokeNonvirtualVoidMethod("finalize.()V", this, null);
    }
}
