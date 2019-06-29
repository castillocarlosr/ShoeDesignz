using AuthorizeNet.Api.Contracts.V1;
using AuthorizeNet.Api.Controllers;
using AuthorizeNet.Api.Controllers.Bases;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ShoeDesignz.Models
{
    public class Payment
    {
        private IConfiguration _configuration;

        public Payment()
        {

        }

        public Payment(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public CreditCardList CreditList { get; set; }
        public enum CreditCardList : long
        {
            //Use 4 digit for top Amex card
            //These numbers are test numbers used by the testing company to verify that transactions work.
            //NOT real credit card numbers!
            Amex_370000000000002 = 370000000000002,
            Discover_6011000000000012 = 6011000000000012,
            Visa_4111111111111111 = 4111111111111111,
            Mastercard_2223000010309703 = 2223000010309703
        }
        public Expiration ExpirationCard { get; set; }
        public enum Expiration
        {
            [Display(Name = "11/19")]
            Oct = 1119,
            [Display(Name = "11/20")]
            Nov = 1120,
            [Display(Name = "12/21")]
            Dec = 1221
        }
        public CCV CCVNumber { get; set; }
        public enum CCV
        {
            [Display(Name = "1234")]
            Amex = 1234,
            [Display(Name = "123")]
            Discover = 123,
            [Display(Name = "234")]
            Visa = 123,
            [Display(Name = "345")]
            MasterCard = 123
        }



        public string Run(long creditCardPassInside, int experationCardIniside)
        {
            ApiOperationBase<ANetApiRequest, ANetApiResponse>.RunEnvironment = AuthorizeNet.Environment.SANDBOX;

            //define merchant information (authentication & transaction ID)
            ApiOperationBase<ANetApiRequest, ANetApiResponse>.MerchantAuthentication = new merchantAuthenticationType()
            {
                name = _configuration["AuthNetAPILogin"],
                ItemElementName = ItemChoiceType.transactionKey,
                Item = _configuration["AuthNetTransactionKey"]
            };

            //CREATE A CREDIT CARD  we need a cc
            //bring in a parameter
            var creditCard = new creditCardType
            {
                //Drop down or have user put in their own CC and check that it's for the future
                cardNumber = creditCardPassInside.ToString(),
                expirationDate = experationCardIniside.ToString()
            };

            customerAddressType billingAddress = new customerAddressType();

            var paymentType = new paymentType { Item = creditCard };

            //transaction request type consolidate all the info before sending to autho.net
            //1. amount of order
            transactionRequestType transactionRequest = new transactionRequestType
            {
                transactionType = transactionTypeEnum.authCaptureTransaction.ToString(),
                amount = 2.34m,
                payment = paymentType,
                billTo = billingAddress,  
                //lineItems = lineItems,
            };

            createTransactionRequest request = new createTransactionRequest
            {
                transactionRequest = transactionRequest
            };

            //Make a call out to Autho.Net
            var controller = new createTransactionController(request);
            //execcute the callll
            controller.Execute();

            //Response from call above
            var response = controller.GetApiResponse();            
            if(response != null)
            {
                
                if (response.messages.resultCode == messageTypeEnum.Ok)
                {
                    if (response.transactionResponse != null)
                    {
                        //Console.WriteLine("Success, Auth Code : " + response.transactionResponse.authCode);
                        return "OK";
                    }
                }
                else
                {
                    Console.WriteLine("Transaction Error : " + response.transactionResponse.errors[0].errorCode + " " + response.transactionResponse.errors[0].errorText);
                    return "This is NOT ok!";
                }
            }

            return "Does not works";
            /*
            // validate response
            if (response != null)
            {
                if (response.messages.resultCode == messageTypeEnum.Ok)
                {
                    if (response.transactionResponse.messages != null)
                    {
                        Console.WriteLine("Successfully created transaction with Transaction ID: " + response.transactionResponse.transId);
                        Console.WriteLine("Response Code: " + response.transactionResponse.responseCode);
                        Console.WriteLine("Message Code: " + response.transactionResponse.messages[0].code);
                        Console.WriteLine("Description: " + response.transactionResponse.messages[0].description);
                        Console.WriteLine("Success, Auth Code : " + response.transactionResponse.authCode);
                    }
                    else
                    {
                        Console.WriteLine("Failed Transaction.");
                        if (response.transactionResponse.errors != null)
                        {
                            Console.WriteLine("Error Code: " + response.transactionResponse.errors[0].errorCode);
                            Console.WriteLine("Error message: " + response.transactionResponse.errors[0].errorText);
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Failed Transaction.");
                    if (response.transactionResponse != null && response.transactionResponse.errors != null)
                    {
                        Console.WriteLine("Error Code: " + response.transactionResponse.errors[0].errorCode);
                        Console.WriteLine("Error message: " + response.transactionResponse.errors[0].errorText);
                    }
                    else
                    {
                        Console.WriteLine("Error Code: " + response.messages.message[0].code);
                        Console.WriteLine("Error message: " + response.messages.message[0].text);
                    }
                }
            }
            else
            {
                Console.WriteLine("Null Response.");
            }

            return "It was submitted.  That's all I can say";*/
        }

        private customerAddressType GetAddress()
        {
            customerAddressType address = new customerAddressType()
            {
                firstName = "Pokemon",
                lastName = "Pikachu",
                address = "123 Pokemon",
                city = "the palace",
                zip = "98119"
            };
            return address;
        }

        private lineItemType[] GetLineItems(List<ShoeDesignz.Models.CartItems> products)
        {
            lineItemType[] lineItems = new lineItemType[products.Count];

            int count = 0;

            foreach (var item in products)
            {
                lineItems[count] = new lineItemType
                {
                    //itemId = productsID
                    itemId = "1",
                    name = "SHoes",
                    quantity = 2,
                    unitPrice = new Decimal(4.00)
                };
                count++;
            }
            return lineItems;
        }
    }
}
