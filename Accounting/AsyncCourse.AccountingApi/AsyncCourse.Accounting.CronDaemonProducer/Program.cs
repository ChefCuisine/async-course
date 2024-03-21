using AsyncCourse.Accounting.CronDaemonProducer;

var cronJob = new CronJobHandler();
var doneOperationPerDay = new Dictionary<DateTime, bool>(); // todo переделать на нормальный крон

while (true)
{
    var lastDate = doneOperationPerDay.Last().Key;

    var dateTime = DateTime.Now;
    var date = dateTime.Date;

    if (date == lastDate)
    {
        await Task.Delay(TimeSpan.FromMinutes(5));
    }
    else
    {
        await cronJob.DoDailyWork();
        doneOperationPerDay.Add(date, true);
    }
}