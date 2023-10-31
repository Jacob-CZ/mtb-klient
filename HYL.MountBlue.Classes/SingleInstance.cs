namespace HYL.MountBlue.Classes;

internal class SingleInstance
{
	internal bool IsRunning => false;

	internal SingleInstance(string mutexname)
	{
	}

	internal void ReleaseMutex()
	{
	}
}
