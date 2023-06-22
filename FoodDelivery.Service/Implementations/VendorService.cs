using FoodDelivery.DAL.Entity;
using FoodDelivery.DAL;
using FoodDelivery.Models.ViewModel.DTOs;
using FoodDelivery.Service.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FoodDelivery.Service.Implementations
{
    public class VendorService : IVendorService
    {
        public readonly DataContext _db;
        public VendorService(DataContext db)
        {
            _db = db;
        }
        public async Task<IEnumerable<Vendor>> GetListAsync()
        {
            try
            {
                var vendors = await _db.Vendors.AsNoTracking().ToListAsync();
                return vendors;
            }
            catch (Exception ex)
            {
                throw new Exception("error when getting an vendor list ", ex);
            }
        }
        public async Task<Vendor> GetByIdAsync(int id)
        {
            try
            {
                var vendor = await _db.Vendors.FirstOrDefaultAsync(x => x.Id == id);
                if (vendor == null)
                    throw new Exception("no vendor found");
                return vendor;
            }
            catch (Exception ex)
            {
                throw new Exception("vendor search error ", ex);
            }
        }
        public async Task<bool> CreateAsync(VendorDto vendorDto)
        {
            try
            {
                Vendor vendor = new Vendor();
                vendor.Type = vendorDto.Type;
                vendor.Name = vendorDto.Name;
                vendor.PhoneNumber = vendorDto.PhoneNumber;
                vendor.Address = vendorDto.Address;
                vendor.OpeningTime = vendorDto.OpeningTime;
                vendor.ClosingTime = vendorDto.ClosingTime;
                vendor.TimeOfDelivery = vendorDto.TimeOfDelivery;
                vendor.Description = vendorDto.Description;
                _db.Vendors.Add(vendor);
                return await SaveAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("error when creating an vendor ", ex);
            }
        }
        public async Task<bool> UpdateAsync(Vendor vendor)
        {
            try
            {
                _db.Update(vendor);
                return await SaveAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("error when changing an vendor ", ex);
            }
        }
        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                var vendor = await _db.Vendors.FirstOrDefaultAsync(c => c.Id == id);
                if (vendor == null)
                    throw new Exception("no vendor found");
                _db.Vendors.Remove(vendor);
                return await SaveAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("error when deleting an vendor ", ex);
            }
        }
        public async Task<bool> SaveAsync()
        {
            try
            {
                var saved = await _db.SaveChangesAsync();
                return saved > 0 ? true : false;
            }
            catch (Exception ex)
            {
                throw new Exception("save error ", ex);
            }
        }
    }
}
