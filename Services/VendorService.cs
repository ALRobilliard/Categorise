using System;
using System.Collections.Generic;
using System.Linq;
using Categorise.Data;

namespace Categorise.Services
{
    /// <summary>
    /// Service for exposing common actions for Vendor.
    /// </summary>
    public interface IVendorService
    {
        /// <summary>
        /// Retrieve all vendors for the specified user.
        /// </summary>
        IEnumerable<Vendor> GetVendors(string userId);

        /// <summary>
        /// Retrieve a single vendor for the specified user by vendor unique identifier.
        /// </summary>
        Vendor GetVendorById(Guid id, string userId);

        /// <summary>
        /// Retrieve a single vendor for the specified user by vendor name.
        /// </summary>
        Vendor GetVendorByName(string name, string userId);

        /// <summary>
        /// Creates an vendor for the specified user.
        /// </summary>
        Vendor CreateVendor(Vendor vendor, string userId);

        /// <summary>
        /// Updates an vendor for the specified user.
        /// </summary>
        void UpdateVendor(Vendor vendor, string userId);

        /// <summary>
        /// Deletes the specified vendor for the specified user.
        /// </summary>
        void DeleteVendor(Guid vendorId, string userId);
    }

    /// <summary>
    /// Service for exposing common actions for Vendor.
    /// </summary>
    public class VendorService : IVendorService
    {
        private CategoriseContext _context;

        /// <summary>
        /// Constructor for the VendorService.
        /// </summary>
        public VendorService(CategoriseContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Retrieve all vendors for the specified user.
        /// </summary>
        public IEnumerable<Vendor> GetVendors(string userId)
        {
            return _context.Vendors.Where(v => v.UserId == userId);
        }

        /// <summary>
        /// Retrieve a single vendor for the specified user by vendor unique identifier.
        /// </summary>
        public Vendor GetVendorById(Guid id, string userId)
        {
            return _context.Vendors.Where(v => v.Id == id && v.UserId == userId).FirstOrDefault();
        }

        /// <summary>
        /// Retrieve a single vendor for the specified user by vendor name.
        /// </summary>
        public Vendor GetVendorByName(string name, string userId)
        {
            return _context.Vendors
              .Where(v => v.VendorName == name && v.UserId == userId).FirstOrDefault();
        }

        /// <summary>
        /// Creates an vendor for the specified user.
        /// </summary>
        public Vendor CreateVendor(Vendor vendorDto, string userId)
        {
            Vendor vendor = new Vendor
            {
                VendorName = vendorDto.VendorName,
                UserId = userId
            };
            _context.Vendors.Add(vendor);
            _context.SaveChanges();

            return vendor;
        }

        /// <summary>
        /// Updates an vendor for the specified user.
        /// </summary>
        public void UpdateVendor(Vendor vendorDto, string userId)
        {
            Vendor vendor = _context.Vendors.Find(vendorDto.Id);

            if (vendor != null)
            {
                vendor.VendorName = vendorDto.VendorName;

                _context.Vendors.Update(vendor);
                _context.SaveChanges();
            }
        }

        /// <summary>
        /// Deletes the specified vendor for the specified user.
        /// </summary>
        public void DeleteVendor(Guid vendorId, string userId)
        {
            Vendor vendor = _context.Vendors.Find(vendorId);

            if (vendor != null)
            {
                _context.Vendors.Remove(vendor);
                _context.SaveChanges();
            }
        }
    }
}