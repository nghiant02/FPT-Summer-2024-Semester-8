using EXE201.DAL.Interfaces;
using EXE201.DAL.Models;
using MCC.DAL.Repository.Implements;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EXE201.DAL.DTOs.UserDTOs;

namespace EXE201.DAL.Repository
{

    //public class MembershipRepository : GenericRepository<Membership>, IMembershipRepository
    //{
    //    public MembershipRepository(EXE201Context context) : base(context)
    //    {
    //    }

    //    public async Task<IEnumerable<Membership>> GetAll()
    //    {
    //        try
    //        {
    //            var membership = await _context.Memberships
    //                .Include(x => x.User)
    //                .Include(x => x.MembershipType)
    //                .ToListAsync();
    //            return membership;
    //        }
    //        catch (Exception ex)
    //        {
    //            throw new Exception(ex.Message);
    //        }
    //    }

    //    public async Task<MembershipUserDto> GetMembershipByUserId(int userId)
    //    {
    //        try
    //        {
    //            var membership = await _context.Memberships
    //                .Where(x => x.UserId == userId)
    //                .Include(x => x.MembershipType)
    //                .Select(x => new MembershipUserDto
    //                {
    //                    MembershipId = x.MembershipId,
    //                    UserId = x.UserId,
    //                    MembershipTypeId = x.MembershipType.MembershipTypeId,
    //                    MembershipTypeName = x.MembershipType.MembershipTypeName,
    //                    MembershipTypeDescription = x.MembershipType.MembershipDescription,
    //                    MembershipTypeBenefits = x.MembershipType.MembershipBenefits,
    //                    MembershipStatus = x.MembershipStatus,
    //                    StartDate = x.StartDate
    //                })
    //                .FirstOrDefaultAsync();
    //            if (membership != null)
    //            {
    //                return membership;
    //            }
    //            return null;
    //        }
    //        catch (Exception e)
    //        {
    //            throw new Exception(e.Message);
    //        }
    //    }
    //}
}
