using System;

namespace ApiTools.HttpTools
{
    public class UriTest
    {
        Uri uri;


        public UriTest(Uri uri)
        {
            this.uri = uri;
        }


        public bool DoesExist()
        {
            try
            {
                var client = new HttpClientWrapper(uri).FetchResponse();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
