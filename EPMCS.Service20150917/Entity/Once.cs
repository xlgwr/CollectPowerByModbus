namespace EPMCS.Service.Entity
{
    public class Once
    {
        private string groupstamp;

        public string Groupstamp
        {
            get { return groupstamp; }
            set { groupstamp = value; }
        }

        private int status;

        public int Status
        {
            get { return status; }
            set { status = value; }
        }
    }
}