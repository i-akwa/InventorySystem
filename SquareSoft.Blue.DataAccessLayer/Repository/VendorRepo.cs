using Blue.Models;
using SquareSoft.Blue.DataAccessLayer.Data;
using SquareSoft.Blue.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SquareSoft.Blue.DataAccessLayer.Repository
{
    
    public class VendorRepo
    {
        static Vendors singleVendor;
        internal static ApplicationDataContext _context = new ApplicationDataContext();
        public DbSet<Vendors> dbset;

        public VendorRepo(ApplicationDataContext context)
        {
            _context = context;
            this.dbset = context.Set<Vendors>();
        }

        public static int AddVendor(string compName, string manName, string address, DateTime lastEdited, string telNum)
        {
            int returns = 0;
            try
            {
                var vendor = new Vendors
                {
                    Id = Guid.NewGuid(),
                    CreatedDate = System.DateTime.UtcNow.Date,
                    VendorCopName = compName,
                    HeadPersonName = manName,
                    HQAddress = address,
                    LastEdited = lastEdited,
                    Tel = telNum
                };
                _context.vendors.Add(vendor);
                return returns = _context.SaveChanges();
            }
            catch(Exception)
            {
                throw;
            }
        }

        public List<VendorSelect> GetVendors()
        {
            var data = (from v in _context.vendors
                        select new VendorSelect
                        {
                            Id = v.Id,
                            HeadPersonName = v.HeadPersonName,
                            HQAddress = v.HQAddress,                           
                            Tel = v.Tel,
                            VendorCopName = v.VendorCopName,
                            CreatedDate = v.CreatedDate
                        });
            List<VendorSelect> listVen = data.ToList();
            return listVen;
        }

        public virtual void DeleteVendor(int id)
        {
            var entity = _context.vendors.Find(id);
            Delete(entity);
        }

        public virtual void Delete(Vendors entity)
        {
            try
            {
                if (_context.Entry(entity).State == EntityState.Detached)
                {
                    _context.vendors.Attach(entity);
                }
                _context.vendors.Remove(entity);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static int UpdateUserRecord(Guid id, string column, string value)
        {
            try
            {
                int saved = 0;
                
                singleVendor = _context.vendors.Where(v => v.Id == id).FirstOrDefault<Vendors>();
                
                if (singleVendor != null)
                {
                    if (column == "HeadPersonName")
                    { singleVendor.HeadPersonName = value; }

                    else if (column == "HQAddress")
                    { singleVendor.HQAddress = value; }

                    else if (column == "Tel")
                    { singleVendor.Tel = value; }

                    _context.Entry(singleVendor).State = System.Data.Entity.EntityState.Modified;
                    saved = _context.SaveChanges();
                    
                }
                return saved;
            }
            catch (Exception)
            {

                throw;
            }
        }

    }

    public class VendorSelect
    {
        public Guid Id { get; set; }
        public string HeadPersonName { get; set; }
        public string HQAddress { get; set; }
        
        public string Tel { get; set; }
        public string VendorCopName { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
