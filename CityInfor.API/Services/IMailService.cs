﻿namespace CityInfor.API.Services
{
    public interface IMailService
    {
        void Send(string subject, string message);
    }
}