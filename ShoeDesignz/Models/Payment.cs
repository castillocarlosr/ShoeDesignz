﻿using AuthorizeNet.Api.Contracts.V1;
using AuthorizeNet.Api.Controllers;
using AuthorizeNet.Api.Controllers.Bases;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoeDesignz.Models
{
    public class Payment
    {
        private IConfiguration _configuration;

        public Payment(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public CreditCardList CreditList { get; set; }
        public enum CreditCardList : long
        {  
            //Use 4 digit for top Amex card
            Amex = 370000000000002,
            Discover = 6011000000000012,
            Visa = 4111111111111111,
            Mastercard = 2223000010309703
        }
        public Expiration ExpirationCard { get; set; }
        public enum Expiration
        {
            Oct = 1020,
            Nov = 1120,
            Dec = 1222
        }



        public string Run()
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
                cardNumber = CreditList.ToString(),
                //Drop down or have user put in their own CC and check that it's for the future
                expirationDate = ExpirationCard.ToString()
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
                        return "OK!";
                    }
                }
                else
                {
                    return "This is NOT ok!";
                }
            }

            return "Does not works";
        }

        private customerAddressType GetAddress()
        {
            customerAddressType address = new customerAddressType()
            {
                firstName = "Pokemon",
                lastName = "Pikachu"
            };
            return address;
        }

        //private lineItemType[] GetLineItems(List<Post> products)
        //{
        //    lineItemType[] lineItems = new lineItemType[products.Count];

        //    int count = 0;

        //    foreach (var item in products)
        //    {
        //        lineItems[count] = new lineItemType
        //        {
        //            //itemId = productsID
        //            itemId = "1",
        //            name = "SHoes"
        //        };
        //        count++;
        //    }
        //    return lineItems;
        //}
    }
}
