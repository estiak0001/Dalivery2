using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAppEs.Data;
using WebAppEs.Entity;
using WebAppEs.ViewModel.Booking;
using WebAppEs.ViewModel.Courier;
using WebAppEs.ViewModel.Customer;
using WebAppEs.ViewModel.EmployeeInformation;
using WebAppEs.ViewModel.Indexing;
using WebAppEs.ViewModel.ProductModel;
using WebAppEs.ViewModel.SalesChannel;
using WebAppEs.ViewModel.Zone;

namespace WebAppEs.Services
{
    public class SetupService : ISetupService
    {
        private readonly ApplicationDbContext _context;
        public SetupService(ApplicationDbContext context)
        {
            _context = context;
        }
        public bool AddCourierInfo(MobileRND_CourierInformation_VM viewModel)
        {
            var result = 0;
            var UpdateDataSet = _context.MobileRND_CourierInformation.Where(x => x.Id == viewModel.Id).FirstOrDefault();
            var existing = _context.MobileRND_CourierInformation.Where(x => x.CourierName == viewModel.CourierName).FirstOrDefault();
            if (UpdateDataSet != null)
            {
                UpdateDataSet.CourierName = viewModel.CourierName;
                UpdateDataSet.CoverRate = (Decimal)viewModel.CoverRate;
                UpdateDataSet.NonCoverRate = (Decimal)viewModel.NonCoverRate;
                UpdateDataSet.RateFixedFromDate = (DateTime)viewModel.RateFixedFromDate;
                UpdateDataSet.RateFixedToDate = (DateTime)viewModel.RateFixedToDate;
                UpdateDataSet.Address = viewModel.Address;
                UpdateDataSet.ContactNumber = viewModel.ContactNumber;
                _context.MobileRND_CourierInformation.Update(UpdateDataSet);
                result = _context.SaveChanges();
            }
            else
            {
                if (existing != null)
                {
                    return false;
                }

                _context.MobileRND_CourierInformation.Add(new MobileRND_CourierInformation()
                {
                    CourierName = viewModel.CourierName,
                    CoverRate = (Decimal)viewModel.CoverRate,
                    NonCoverRate = (Decimal)viewModel.NonCoverRate,
                    RateFixedFromDate = (DateTime)viewModel.RateFixedFromDate,
                    RateFixedToDate = (DateTime)viewModel.RateFixedToDate,
                    Address = viewModel.Address,
                    ContactNumber = viewModel.ContactNumber,

                });
                result = _context.SaveChanges();
            }
            return result > 0;
        }

        public bool AddCustomerInfo(MobileRND_CustomerInfo_VM viewModel)
        {
            var result = 0;
            var UpdateDataSet = _context.MobileRND_CustomerInfo.Where(x => x.Id == viewModel.Id).FirstOrDefault();
            var existing = _context.MobileRND_CustomerInfo.Where(x => x.CustomerNo == viewModel.CustomerNo).FirstOrDefault();
            if (UpdateDataSet != null)
            {
                UpdateDataSet.CustomerName = viewModel.CustomerName;
                UpdateDataSet.SalesChannelID = viewModel.SalesChannelID;
                UpdateDataSet.ZoneID = viewModel.ZoneID;
                UpdateDataSet.Product = viewModel.ProductID;
                UpdateDataSet.Brand = viewModel.BrandID;
                UpdateDataSet.Address = viewModel.Address;
                UpdateDataSet.DeliveryAddress = viewModel.DeliveryAddress;
                UpdateDataSet.PhoneNo = viewModel.PhoneNo;
                UpdateDataSet.DeliveryDetails = viewModel.DeliveryDetails;
                _context.MobileRND_CustomerInfo.Update(UpdateDataSet);
                result = _context.SaveChanges();
            }
            else
            {
                if (existing != null)
                {
                    return false;
                }

                _context.MobileRND_CustomerInfo.Add(new MobileRND_CustomerInfo()
                {
                    CustomerNo = viewModel.CustomerNo,
                    CustomerName = viewModel.CustomerName,
                    SalesChannelID = viewModel.SalesChannelID,
                    ZoneID = viewModel.ZoneID,
                    Product = viewModel.ProductID,
                    Brand = viewModel.BrandID,
                    Address = viewModel.Address,
                    DeliveryAddress = viewModel.DeliveryAddress,
                    PhoneNo = viewModel.PhoneNo,
                    DeliveryDetails = viewModel.DeliveryDetails
                });
                result = _context.SaveChanges();
            }
            return result > 0;
        }

        public bool AddEmployeeInfo(MobileRND_EmployeeInformation_VM viewModel)
        {
            var result = 0;
            var UpdateDataSet = _context.MobileRND_EmployeeInformation.Where(x => x.Id == viewModel.Id).FirstOrDefault();
            var existing = _context.MobileRND_EmployeeInformation.Where(x => x.EmployeeID == viewModel.EmployeeID).FirstOrDefault();
            if (UpdateDataSet != null)
            {
                UpdateDataSet.EmployeeID = viewModel.EmployeeID;
                UpdateDataSet.EmployeeName = viewModel.EmployeeName;
                UpdateDataSet.ContactNumber = viewModel.ContactNumber;
                //UpdateDataSet.SalesChannelID = viewModel.SalesChannelID;
                UpdateDataSet.EmployeeType = viewModel.EmployeeType;
                //UpdateDataSet.ZoneID = viewModel.ZoneID;
                UpdateDataSet.Brand = viewModel.BrandID;
                
                _context.MobileRND_EmployeeInformation.Update(UpdateDataSet);
                result = _context.SaveChanges();
            }
            else
            {
                if (existing != null)
                {
                    return false;
                }

                _context.MobileRND_EmployeeInformation.Add(new MobileRND_EmployeeInformation()
                {
                    EmployeeID = viewModel.EmployeeID,
                    EmployeeName = viewModel.EmployeeName,
                    ContactNumber = viewModel.ContactNumber,
                    //SalesChannelID = viewModel.SalesChannelID,
                    EmployeeType = viewModel.EmployeeType,
                    //ZoneID = viewModel.ZoneID,
                    Brand = viewModel.BrandID
                });
                result = _context.SaveChanges();
            }
            return result > 0;
        }

        public async Task<bool> AddItemInformation(MobileRND_Items_VM viewModel)
        {
            var result = 0;
            var UpdateDataSet = await _context.MobileRND_Items.Where(x => x.Id == viewModel.Id).FirstOrDefaultAsync();
            var existing = await _context.MobileRND_Items.Where(x => x.Brand == viewModel.Brand && x.ModelId == viewModel.ModelId).FirstOrDefaultAsync();

            if (UpdateDataSet != null)
            {
                UpdateDataSet.Brand = viewModel.Brand;
                //UpdateDataSet.ModelId = viewModel.ModelId;
                UpdateDataSet.ItemCode = viewModel.ItemCode;
                UpdateDataSet.ItemName = viewModel.ItemName;
                //UpdateDataSet.Grade = viewModel.Grade;

                _context.MobileRND_Items.Update(UpdateDataSet);
                result = await _context.SaveChangesAsync();
            }
            else
            {
                if (existing != null)
                {
                    return false;
                }
                else
                {
                    _context.MobileRND_Items.Add(new MobileRND_Items()
                    {
                        Brand = viewModel.Brand,
                        ModelId = viewModel.ModelId,
                        ItemCode = viewModel.ItemCode,
                        ItemName = viewModel.ItemName
                    });
                    result = await _context.SaveChangesAsync();
                }
            }
            return result > 0;
        }

        public async Task<bool> AddPartsModel(ProductModel_VM viewModel)
        {
            var result = 0;
            var UpdateDataSet = await _context.MobileRND_ProductModel.Where(x => x.ID == viewModel.ID).FirstOrDefaultAsync();
            var existing = await _context.MobileRND_ProductModel.Where(x => x.Name == viewModel.Name).FirstOrDefaultAsync();

            if (UpdateDataSet != null)
            {
                UpdateDataSet.Name = viewModel.Name;
                _context.MobileRND_ProductModel.Update(UpdateDataSet);
                result = await _context.SaveChangesAsync();
            }
            else
            {
                if (existing != null)
                {
                    return false;
                }
                else
                {
                    _context.MobileRND_ProductModel.Add(new MobileRND_ProductModel()
                    {
                        Name = viewModel.Name,
                    });
                    result = await _context.SaveChangesAsync();
                }
            }
            return result > 0;
        }

        public bool AddSalesChannel(MobileRND_SalesChannel_VM viewModel)
        {
            var result = 0;
            var UpdateDataSet =  _context.MobileRND_SalesChannel.Where(x => x.Id == viewModel.Id).FirstOrDefault();
            var existing =  _context.MobileRND_SalesChannel.Where(x => x.ChannelName == viewModel.ChannelName).FirstOrDefault();

            if (existing != null)
            {
                return false;
            }
            else
            {
                if (UpdateDataSet != null)
                {
                    UpdateDataSet.ChannelName = viewModel.ChannelName;

                    _context.MobileRND_SalesChannel.Update(UpdateDataSet);
                    result =  _context.SaveChanges();
                }
                else
                {
                    _context.MobileRND_SalesChannel.Add(new MobileRND_SalesChannel()
                    {
                        ChannelName = viewModel.ChannelName
                    });
                    result =  _context.SaveChanges();
                }
            }
            return result > 0;
        }

        public bool AddZone(MobileRND_Zone_VM viewModel)
        {
            var result = 0;
            var UpdateDataSet = _context.MobileRND_Zone.Where(x => x.Id == viewModel.Id).FirstOrDefault();
            var existing = _context.MobileRND_Zone.Where(x => x.ZoneName == viewModel.ZoneName).FirstOrDefault();

            if (existing != null)
            {
                return false;
            }
            else
            {
                if (UpdateDataSet != null)
                {
                    UpdateDataSet.ZoneName = viewModel.ZoneName;

                    _context.MobileRND_Zone.Update(UpdateDataSet);
                    result = _context.SaveChanges();
                }
                else
                {
                    _context.MobileRND_Zone.Add(new MobileRND_Zone()
                    {
                        ZoneName = viewModel.ZoneName
                    });
                    result = _context.SaveChanges();
                }
            }
            return result > 0;
        }

        public List<MobileRND_Brand_VM> GetAllBrand()
        {
            var items = (from br in _context.MobileRND_Brand

                         select new MobileRND_Brand_VM()
                         {
                             Id = br.Id,
                             BrandName = br.BrandName,
                         }).ToList();

            return items;
        }

        public List<MobileRND_CourierInformation_VM> GetAllCourierList()
        {
            var items = (from cu in _context.MobileRND_CourierInformation
                         
                         select new MobileRND_CourierInformation_VM()
                         {
                             Id = cu.Id,
                             CourierName = cu.CourierName,
                             CoverRate = cu.CoverRate,
                             NonCoverRate = cu.NonCoverRate,
                             RateFixedFromDate = cu.RateFixedFromDate,
                             RateFixedToDate = cu.RateFixedToDate,
                             Address = cu.Address,
                             ContactNumber = cu.ContactNumber,
                         }).ToList();

            return items;
        }

        public List<MobileRND_CustomerInfo_VM> GetAllCustomerList()
        {
            var items = (from cu in _context.MobileRND_CustomerInfo
                         join ch in _context.MobileRND_SalesChannel
                                on new { X1 = cu.SalesChannelID } equals new { X1 = ch.Id }
                                into rmp
                         from rm in rmp.DefaultIfEmpty()
                         join zn in _context.MobileRND_Zone
                                on new { X1 = cu.ZoneID } equals new { X1 = zn.Id }
                                into znrmp
                         from znrm in znrmp.DefaultIfEmpty()

                         join pro in _context.MobileRND_Product
                                on new { X1 = cu.Product } equals new { X1 = pro.Id }
                                into znpro
                         from znrmpro in znpro.DefaultIfEmpty()

                         join bra in _context.MobileRND_Brand
                                on new { X1 = cu.Brand } equals new { X1 = bra.Id }
                                into znbra
                         from znrmbra in znbra.DefaultIfEmpty()

                         select new MobileRND_CustomerInfo_VM()
                         {
                             Id = cu.Id,
                             CustomerNo = cu.CustomerNo,
                             CustomerName = cu.CustomerName,
                             SalesChannelID = cu.SalesChannelID,
                             SalseChannelName = rm.ChannelName,
                             ZoneID = cu.ZoneID,
                             ZoneName = znrm.ZoneName,
                             ProductID = cu.Product,
                             BrandID = cu.Brand,
                             Product = znrmpro.ProductName,
                             Brand = znrmbra.BrandName,
                             Address = cu.Address,
                             DeliveryAddress = cu.DeliveryAddress,
                             PhoneNo = cu.PhoneNo,
                             DeliveryDetails = cu.DeliveryDetails,

                         }).ToList();

            return items;
        }

        public List<MobileRND_CustomerInfo_VM> GetAllCustomerListByChannelAndZone(Guid channel, Guid Zone)
        {
            var items = (from cu in _context.MobileRND_CustomerInfo.Where(x=> x.SalesChannelID == channel && x.ZoneID == Zone)
                         join ch in _context.MobileRND_SalesChannel
                                on new { X1 = cu.SalesChannelID } equals new { X1 = ch.Id }
                                into rmp
                         from rm in rmp.DefaultIfEmpty()
                         join zn in _context.MobileRND_Zone
                                on new { X1 = cu.ZoneID } equals new { X1 = zn.Id }
                                into znrmp
                         from znrm in znrmp.DefaultIfEmpty()

                         join pro in _context.MobileRND_Product
                                on new { X1 = cu.Product } equals new { X1 = pro.Id }
                                into znpro
                         from znrmpro in znpro.DefaultIfEmpty()

                         join bra in _context.MobileRND_Brand
                                on new { X1 = cu.Brand } equals new { X1 = bra.Id }
                                into znbra
                         from znrmbra in znbra.DefaultIfEmpty()
                         select new MobileRND_CustomerInfo_VM()
                         {
                             Id = cu.Id,
                             CustomerNo = cu.CustomerNo,
                             CustomerName = cu.CustomerName + " (" + cu.CustomerNo + ")",
                             SalesChannelID = cu.SalesChannelID,
                             SalseChannelName = rm.ChannelName,
                             ZoneID = cu.ZoneID,
                             ZoneName = znrm.ZoneName,
                             ProductID = cu.Product,
                             BrandID = cu.Brand,
                             Product = znrmpro.ProductName,
                             Brand = znrmbra.BrandName,
                             Address = cu.Address,
                             DeliveryAddress = cu.DeliveryAddress,
                             PhoneNo = cu.PhoneNo,
                             DeliveryDetails = cu.DeliveryDetails
                         }).ToList();

            return items;
        }

        public List<MobileRND_EmployeeInformation_VM> GetAllEmployeeList()
        {
            var items = (from cu in _context.MobileRND_EmployeeInformation

                         join br in _context.MobileRND_Brand
                                on new { X1 = cu.Brand } equals new { X1 = br.Id }
                                into znrmp
                         from znrm in znrmp.DefaultIfEmpty()
                         select new MobileRND_EmployeeInformation_VM()
                         {
                             Id = cu.Id,
                             EmployeeID = cu.EmployeeID,
                             EmployeeName = cu.EmployeeName,
                             //SalesChannelID = cu.SalesChannelID,
                             //SalseChannelName = rm.ChannelName,
                             EmployeeType = cu.EmployeeType,
                             //ZoneID = cu.ZoneID,
                             BrandID = cu.Brand,
                             Brand = znrm.BrandName,
                             ContactNumber = cu.ContactNumber
                         }).ToList();

            return items;
        }

        public List<MobileRND_EmployeeInformation_VM> GetAllEmployeeListByType(string EmployeeType)
        {
            var items = (from cu in _context.MobileRND_EmployeeInformation.Where(x=> x.EmployeeType == EmployeeType)
                         join br in _context.MobileRND_Brand
                            on new { X1 = cu.Brand } equals new { X1 = br.Id }
                            into znrmp
                         from znrm in znrmp.DefaultIfEmpty()
                         select new MobileRND_EmployeeInformation_VM()
                         {
                             Id = cu.Id,
                             EmployeeID = cu.EmployeeID,
                             EmployeeName = cu.EmployeeName +" ("+cu.EmployeeType+" - "+cu.EmployeeID+")",
                             //SalesChannelID = cu.SalesChannelID,
                             //SalseChannelName = rm.ChannelName,
                             EmployeeType = cu.EmployeeType,
                             //ZoneID = cu.ZoneID,
                             //ZoneName = znrm.ZoneName,
                             BrandID = cu.Brand,
                             Brand = znrm.BrandName,
                             ContactNumber = cu.ContactNumber
                         }).ToList();

            return items;
        }

        public List<MobileRND_Items_VM> GetAllItemsList()
        {
            var items = (from gwi in _context.MobileRND_Items
                         join mm in _context.MobileRND_Brand
                            on new { X1 = gwi.Brand } equals new { X1 = mm.Id }
                            into znrmp
                         from znrm in znrmp.DefaultIfEmpty()
                         join mm2 in _context.MobileRND_ProductModel
                            on new { X1 = gwi.ModelId } equals new { X1 = mm2.ID }
                            into mod
                         from model in mod.DefaultIfEmpty()
                         select new MobileRND_Items_VM()
                         {
                             Id = gwi.Id,
                             Brand = gwi.Brand,
                             ModelId = gwi.ModelId,
                             ItemCode = gwi.ItemCode,
                             ItemName = gwi.ItemName,
                             BrandName = znrm.BrandName,
                             ModelName = model.Name,
                             ItemNameWithItemCode = gwi.ItemName + " (" + gwi.ItemCode + ")"
                         }).ToList();
            return items;
        }

        public List<ProductModel_VM> GetAllPartsModelList()
        {
            var items = (from partModel in _context.MobileRND_ProductModel
                         select new ProductModel_VM()
                         {
                             ID = partModel.ID,
                             Name = partModel.Name,
                         }).ToList();
            return items;
        }

        public List<MobileRND_PaymentType_VM> GetAllPaymentType()
        {
            var items = (from py in _context.MobileRND_PaymentType                           
                         select new MobileRND_PaymentType_VM()
                         {
                             Id = py.Id,
                             TypeName = py.TypeName,
                         }).ToList();
            return items;
        }

        public List<MobileRND_Product_VM> GetAllProduct()
        {
            var items = (from pr in _context.MobileRND_Product

                         select new MobileRND_Product_VM()
                         {
                             Id = pr.Id,
                             ProductName = pr.ProductName,
                         }).ToList();

            return items;
        }

        public List<MobileRND_SalesChannel_VM> GetAllSalseChannelList()
        {
            var items = (from ch in _context.MobileRND_SalesChannel

                         select new MobileRND_SalesChannel_VM()
                         {
                             Id = ch.Id,
                             ChannelName = ch.ChannelName
                         }).ToList();
            return items;
        }

        public List<MobileRND_Zone_VM> GetAllZoneList()
        {
            var items = (from zn in _context.MobileRND_Zone

                         select new MobileRND_Zone_VM()
                         {
                             Id = zn.Id,
                             ZoneName = zn.ZoneName
                         }).ToList();

            return items;
        }

        public MobileRND_CourierInformation_VM GetCourier(Guid Id)
        {
            var items = (from cu in _context.MobileRND_CourierInformation.Where(c=> c.Id == Id)

                         select new MobileRND_CourierInformation_VM()
                         {
                             Id = cu.Id,
                             CourierName = cu.CourierName,
                             CoverRate = cu.CoverRate,
                             NonCoverRate = cu.NonCoverRate,
                             RateFixedFromDate = cu.RateFixedFromDate,
                             RateFixedToDate = cu.RateFixedToDate,
                             Address = cu.Address,
                             ContactNumber = cu.ContactNumber,
                         }).FirstOrDefault();

            return items;
        }

        public GetCourierRate GetCourierRateByCourierIDAndType(Guid CourierID, string Couriertype)
        {
            var items = (from cu in _context.MobileRND_CourierInformation.Where(c => c.Id == CourierID)

                         select new GetCourierRate()
                         {
                             Rate = Couriertype == "Cover" ? cu.CoverRate : cu.NonCoverRate
                         }).FirstOrDefault();
            return items;
        }

        public MobileRND_CustomerInfo_VM GetCustomer(Guid Id)
        {
            var items = (from cu in _context.MobileRND_CustomerInfo.Where(p=> p.Id == Id)
                         join ch in _context.MobileRND_SalesChannel
                                on new { X1 = cu.SalesChannelID } equals new { X1 = ch.Id }
                                into rmp
                         from rm in rmp.DefaultIfEmpty()
                         join zn in _context.MobileRND_Zone
                                on new { X1 = cu.ZoneID } equals new { X1 = zn.Id }
                                into znrmp
                         from znrm in znrmp.DefaultIfEmpty()
                         join pro in _context.MobileRND_Product
                                on new { X1 = cu.Product } equals new { X1 = pro.Id }
                                into znpro
                         from znrmpro in znpro.DefaultIfEmpty()

                         join bra in _context.MobileRND_Brand
                                on new { X1 = cu.Brand } equals new { X1 = bra.Id }
                                into znbra
                         from znrmbra in znbra.DefaultIfEmpty()
                         select new MobileRND_CustomerInfo_VM()
                         {
                             Id = cu.Id,
                             CustomerNo = cu.CustomerNo,
                             CustomerName = cu.CustomerName,
                             SalesChannelID = cu.SalesChannelID,
                             SalseChannelName = rm.ChannelName,
                             ZoneID = cu.ZoneID,
                             ZoneName = znrm.ZoneName,
                             ProductID = cu.Product,
                             BrandID = cu.Brand,
                             Product = znrmpro.ProductName,
                             Brand = znrmbra.BrandName,
                             Address = cu.Address,
                             DeliveryAddress = cu.DeliveryAddress,
                             PhoneNo = cu.PhoneNo,
                             DeliveryDetails = cu.DeliveryDetails
                         }).FirstOrDefault();

            return items;
        }

        public MobileRND_CustomerInfo_VM GetCustomerByCustomerNo(string CustomerNo)
        {
            var items = (from cu in _context.MobileRND_CustomerInfo.Where(p => p.CustomerNo == CustomerNo)
                         join ch in _context.MobileRND_SalesChannel
                                on new { X1 = cu.SalesChannelID } equals new { X1 = ch.Id }
                                into rmp
                         from rm in rmp.DefaultIfEmpty()
                         join zn in _context.MobileRND_Zone
                                on new { X1 = cu.ZoneID } equals new { X1 = zn.Id }
                                into znrmp
                         from znrm in znrmp.DefaultIfEmpty()
                         join pro in _context.MobileRND_Product
                                on new { X1 = cu.Product } equals new { X1 = pro.Id }
                                into znpro
                         from znrmpro in znpro.DefaultIfEmpty()

                         join bra in _context.MobileRND_Brand
                                on new { X1 = cu.Brand } equals new { X1 = bra.Id }
                                into znbra
                         from znrmbra in znbra.DefaultIfEmpty()
                         select new MobileRND_CustomerInfo_VM()
                         {
                             Id = cu.Id,
                             CustomerNo = cu.CustomerNo,
                             CustomerName = cu.CustomerName,
                             SalesChannelID = cu.SalesChannelID,
                             SalseChannelName = rm.ChannelName,
                             ZoneID = cu.ZoneID,
                             ZoneName = znrm.ZoneName,
                             ProductID = cu.Product,
                             BrandID = cu.Brand,
                             Product = znrmpro.ProductName,
                             Brand = znrmbra.BrandName,
                             Address = cu.Address,
                             DeliveryAddress = cu.DeliveryAddress,
                             PhoneNo = cu.PhoneNo
                         }).FirstOrDefault();
            return items;
        }

        public MobileRND_EmployeeInformation_VM GetEmployee(Guid Id)
        {
            var items = (from cu in _context.MobileRND_EmployeeInformation.Where(e=> e.Id == Id)
                         join br in _context.MobileRND_Brand
                                 on new { X1 = cu.Brand } equals new { X1 = br.Id }
                                 into znrmp
                         from znrm in znrmp.DefaultIfEmpty()
                         select new MobileRND_EmployeeInformation_VM()
                         {
                             Id = cu.Id,
                             EmployeeID = cu.EmployeeID,
                             EmployeeName = cu.EmployeeName,
                             //SalesChannelID = cu.SalesChannelID,
                             //SalseChannelName = rm.ChannelName,
                             EmployeeType = cu.EmployeeType,
                             BrandID = cu.Brand,
                             Brand = znrm.BrandName,
                             ContactNumber = cu.ContactNumber
                         }).FirstOrDefault();

            return items;
        }

        public MobileRND_Items_VM GetItem(Guid Id)
        {
            var items = (from gwi in _context.MobileRND_Items.Where(x=> x.Id == Id)
                         join mm in _context.MobileRND_Brand
                            on new { X1 = gwi.Brand } equals new { X1 = mm.Id }
                            into znrmp
                         from znrm in znrmp.DefaultIfEmpty()
                         join mm2 in _context.MobileRND_ProductModel
                            on new { X1 = gwi.ModelId } equals new { X1 = mm2.ID }
                            into mod
                         from model in mod.DefaultIfEmpty()
                         select new MobileRND_Items_VM()
                         {
                             Id = gwi.Id,
                             Brand = gwi.Brand,
                             ModelId = gwi.ModelId,
                             ItemCode = gwi.ItemCode,
                             ItemName = gwi.ItemName,
                             BrandName = znrm.BrandName,
                             ModelName = model.Name,
                             IsUpdate = "Update"
                         }).FirstOrDefault();
            return items;
        }

        public ProductModel_VM GetPartsModelList(Guid Id)
        {
            var items = (from partModel in _context.MobileRND_ProductModel.Where(x => x.ID == Id)

                         select new ProductModel_VM()
                         {
                             ID = partModel.ID,
                             Name = partModel.Name,
                             IsUpdate = "Update"
                         }).FirstOrDefault();

            return items;
        }

        public MobileRND_SalesChannel_VM GetSalseChannel(Guid Id)
        {
            var items = (from ch in _context.MobileRND_SalesChannel.Where(x=> x.Id == Id)

                         select new MobileRND_SalesChannel_VM()
                         {
                             Id = ch.Id,
                             ChannelName = ch.ChannelName
                         }).FirstOrDefault();

            return items;
        }

        public MobileRND_Zone_VM GetZone(Guid Id)
        {
            var items = (from zn in _context.MobileRND_Zone.Where(z=> z.Id == Id)

                         select new MobileRND_Zone_VM()
                         {
                             Id = zn.Id,
                             ZoneName = zn.ZoneName
                         }).FirstOrDefault();

            return items;
        }

        
    }
}
