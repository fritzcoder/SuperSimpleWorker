using System;
using System.IO;
using NUnit.Framework;
using delayed_job;

namespace delay_job
{
	[TestFixture()]
	public class JobTest
	{
		string connectionString = 
		 "URI=file:/Users/Fritz/Documents/Projects/delayed_job/delay_job_test/bin/Debug/delay_job.db";

		[Test()]
		public void TestEnqueue ()
		{
			Job.Repository = new RepositorySQLite (connectionString);
			Job.Enqueue(new Ajob("Fritz"));
		}

		[Test()]
		public void TestWorkOff ()
		{
			Job.WorkOff();
		}

		[Test()]
		public void TestRunWithLock ()
		{
			RepositorySQLite sqlite = new RepositorySQLite(connectionString);
			//Assert.AreEqual("Fritz",Job.RunWithLock(typeof(Ajob),"worker1"));
			//Assert.AreEqual("Fritz",Job.RunWithLock("worker1"));
			//Assert.AreEqual("Hello",typeof(Ajob).ToString());
			Job.Enqueue(new Ajob("TestRunWithLock"));
			Job[] newJobs = sqlite.GetNextReadyJobs(1);
			Assert.AreEqual(true, newJobs[0].RunWithLock(12,"awesome"));
		}

		[Test()]
		public void TestFindAvailable ()
		{
			Job[] jobs = Job.FindAvailable();
			Assert.GreaterOrEqual(jobs.Length,0);
		}

		[Test()]
		public void TestReserveAndRunOneJob()
		{

			Job.ReserveAndRunOneJob();
		}

		[Test()]
		public void TestReschedule()
		{
			RepositorySQLite sqlite = new RepositorySQLite(connectionString);
			Job.Enqueue(new Ajob("Reschedule"));
			Job[] newJobs = sqlite.GetNextReadyJobs(1);
			//newJobs[0].Reschedule("test");
		}
	}

	public class Ajob : IJob
	{
		public string name;
		public Ajob() {}
		public Ajob(string n)
		{
			name = n;
		}

		public string perform()
		{
			//RepositorySQLite sqlite = new RepositorySQLite();
			//Job job = sqlite.GetNextReadyJobs("test");
			//job.locked_by = "run";
			//sqlite.UpdateJob(job);
			File.Create(name);

			return name;
		}
	}
}
