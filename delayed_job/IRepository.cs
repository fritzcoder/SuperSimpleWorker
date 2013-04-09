using System;

namespace delayed_job
{
	interface IRepository
	{
		void CreateDb();
		Job CreateJob(Job job/*, IJob j*/);
		Job GetJob(int pid);
		Job[] GetJobs();
		Job GetNextReadyJob(string workerName);
		void ClearJobs(string workerName);
		void UpdateJob(Job job);
	}
}

