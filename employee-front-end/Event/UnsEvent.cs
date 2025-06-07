namespace employee_front_end.Event
{
    public class UnsEvents
    {      
        public event EventHandler CallBackEvent;
        public void CallBackApplicationUsers()
        {
            CallBackEvent?.Invoke(this, EventArgs.Empty);
        }


    }

}


