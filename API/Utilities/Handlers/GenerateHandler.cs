namespace API.Utilities.Handlers
{
    public static class GenerateHandler
    {
        public static string Nik(string lastNik)
        {
            // You can implement your custom NIK generation logic here.
            // For example, let's assume a simple NIK format: "YYMMDDXXXXX", where YYMMDD represents the birthdate and XXXXX is a sequential number.
            // You can modify this logic according to your requirements.

            // If no employees in the database, start with 1
            int sequenceNumber = 1;

            if (!string.IsNullOrEmpty(lastNik))
            {
                string lastSequenceNumberStr = lastNik.Substring(6);
                if (int.TryParse(lastSequenceNumberStr, out int lastSequenceNumber))
                {
                    // Increment the sequence number by 1
                    sequenceNumber = lastSequenceNumber + 1;
                }
            }

            string birthDatePart = DateTime.Now.ToString("yyMMdd");
            string sequenceNumberPart = sequenceNumber.ToString("D5");

            string newNik = $"{birthDatePart}{sequenceNumberPart}";
            return newNik;
        }
    }
}
