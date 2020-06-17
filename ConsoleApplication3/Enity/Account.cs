﻿using System;

 namespace ConsoleApplication1.Enity
{
    public class Account
    {
        public string ID { get; set; }
        public string AccountNumber { get; set; }
        public double Balance { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public string Salt { get; set; }
        public string Fullname { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public Role Role { get; set; }
        public Status Status { get; set; }
        
        public string CreatedAt { get; set; }
        public string UpdatedAt { get; set; }

        public override string ToString()
        {
            string return_string = ID + " | " + AccountNumber + " | " + Balance +
                                   " | " + Username + " | " + PasswordHash + " | " + Salt + " | ";
            string final_return_string = return_string + Fullname + " | " + Email + " | " + PhoneNumber + " | " + Role +" | "+ Status;
            return final_return_string;
        }
    }
    public enum Role {ADMIN = 1,USER = 0}
    public enum Status {ACTIVE = 1 , DEACTIVE = -1, BANNED = 0}
}