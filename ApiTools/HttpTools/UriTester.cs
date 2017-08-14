using System;

namespace ApiTools.HttpTools
{
    public class UriTester
    {
        Uri uri;


        public UriTester(Uri uri)
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
