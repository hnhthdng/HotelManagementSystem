﻿using DataObject.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DAO
{
    public class BookingDetailDAO : FuminiHotelManagementContext
    {
        //Using Singleton parttern
        private static BookingDetailDAO instance = null;
        private readonly static object instanceLock = new object();
        public static BookingDetailDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new BookingDetailDAO();
                    }
                    return instance;
                }
            }
        }

        public IEnumerable<BookingDetail> GetALlBookingDetail()
        {
            var bookingDetails = new List<BookingDetail>();
            try
            {
                using var context = new FuminiHotelManagementContext();
                bookingDetails = context.BookingDetails.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return bookingDetails;
        }

        public void CreateBookingDetail(BookingDetail bookingDetail)
        {
            try
            {
                using var context = new FuminiHotelManagementContext();
                context.BookingDetails.Add(bookingDetail);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void UpdateBookingDetail(BookingDetail bookingDetail)
        {
            try
            {
                using var context = new FuminiHotelManagementContext();
                context.BookingDetails.Update(bookingDetail);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void DeleteBookingDetail(BookingDetail bookingDetail)
        {
            try
            {
                using var context = new FuminiHotelManagementContext();
                context.BookingDetails.Remove(bookingDetail);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public IEnumerable<BookingDetail> GetALlBookingDetailInReservation(int bookingReservationId)
        {
            var bookingDetails = new List<BookingDetail>();
            try
            {
                using var context = new FuminiHotelManagementContext();
                bookingDetails = context.BookingDetails.Where(c => c.BookingReservationId == bookingReservationId).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return bookingDetails;
        }

        public IEnumerable<BookingDetail> GetBookingDetailByRoomID(int id)
        {
            var bookingDetails = new List<BookingDetail>();
            try
            {
                using var context = new FuminiHotelManagementContext();
                bookingDetails = context.BookingDetails.Where(c => c.RoomId == id).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return bookingDetails;
        }

        public IEnumerable<BookingDetail> GetBookingDetailsBetweenDates(DateOnly startDate, DateOnly endDate)
        {
            using var context = new FuminiHotelManagementContext();

            var bookingDetails = context.BookingDetails
                .Where(bd => bd.StartDate >= startDate && bd.EndDate <= endDate)
                .OrderByDescending(bd => bd.BookingReservationId)
                .ToList();

            return bookingDetails;
        }
    }
}
