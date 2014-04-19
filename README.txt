PureVPN API Wrapper for MaestroPanel PureVPN Module


Reference:
http://www.purevpn.com/purevpn-api.php


Usage:

Create standard new vpn user 90 days.

        public void CreateUser()
        {
            var result = new VpnRequest()
                            .Authentication("apiUserName", "apiPassword")
                            .Package("STANDARD")
                            .Period(90)
                            .Create(); 


            if (result.Result == 1)
            {
                Console.WriteLine("VPN User Created");
                Console.WriteLine("User: {0}", result.User);
                Console.WriteLine("Pass: {0}", result.Password);
            }
        }