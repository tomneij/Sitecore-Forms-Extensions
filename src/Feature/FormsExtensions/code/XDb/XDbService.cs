﻿using System;
using Feature.FormsExtensions.XDb.Model;
using Feature.FormsExtensions.XDb.Repository;
using Sitecore.Analytics;

namespace Feature.FormsExtensions.XDb
{
    public class XDbService : IXDbService
    {
        private readonly IXDbContactRepository xDbContactRepository;

        public XDbService(IXDbContactRepository xDbContactRepository)
        {
            this.xDbContactRepository = xDbContactRepository;
        }

        public void IdentifyCurrent(IXDbContact contact)
        {
            CheckIdentifier(contact);
            Tracker.Current.Session.IdentifyAs(contact.IdentifierSource, contact.IdentifierValue);
            xDbContactRepository.UpdateXDbContact(contact);
        }
        
        public void UpdateOrCreate(IXDbContact contact)
        {
            CheckIdentifier(contact);
            xDbContactRepository.UpdateOrCreateXDbContact(contact);
        }

        public IDetailedXDbContact GetCurrentContact()
        {
            var identifiersCount = Tracker.Current.Contact.Identifiers.Count;
            throw new NotImplementedException();
        }

        private static void CheckIdentifier(IXDbContact contact)
        {
            if (string.IsNullOrEmpty(contact.IdentifierSource) || string.IsNullOrEmpty(contact.IdentifierValue))
            {
                throw new Exception("A contact must have an identifiersource and identifiervalue!");
            }
        }
    }
    
}