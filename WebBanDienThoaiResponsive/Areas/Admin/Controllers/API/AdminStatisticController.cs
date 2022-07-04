using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebBanDienThoaiResponsive.Models;

namespace WebBanDienThoaiResponsive.Areas.Admin.Controllers.API
{
    [RoutePrefix("api/statistic")]
    public class AdminStatisticController : ApiController
    {
        /// <summary>
        /// Sử dụng cho việc lấy số lần mua hàng trong hôm nay/tháng này/năm này/từ trước đến nay
        /// và tổng tiền mua hàng trong hôm nay/tháng này/năm này/từ trước đến nay 
        /// </summary>
        class Something
        {
            public string key { get; set; }
            public long value { get; set; }
        }

        [Route("today")]
        [HttpGet]
        public IHttpActionResult Today()
        {
            using (var context = new Context())
            {
                context.Configuration.ProxyCreationEnabled = false;
                DateTime fromDay = DateTime.Now.Date + new TimeSpan(0, 0, 0);
                DateTime toDay = DateTime.Now.Date + new TimeSpan(23, 59, 59);

                List<Something> countOrderList = new List<Something>();
                List<Something> amountIncomeList = new List<Something>();

                List<Order> orderList = new List<Order>();
                List<MemberAccount> memberAccountList = new List<MemberAccount>();
                if (context.Orders.Any(p => p.DeliveryDate >= fromDay && p.DeliveryDate <= toDay))
                {
                    orderList = context.Orders.Where(p => p.DeliveryDate >= fromDay && p.DeliveryDate <= toDay).ToList();
                }
                if ((from A in context.MemberAccounts
                     join B in context.AccountTypes
                     on A.MemberTypeID equals B.ID
                     where B.UserTypeName != "Admin"
                     select A).Any(p => p.JoinDate >= fromDay && p.JoinDate <= toDay))
                {
                    memberAccountList = (from A in context.MemberAccounts
                                         join B in context.AccountTypes
                                         on A.MemberTypeID equals B.ID
                                         where B.UserTypeName != "Admin"
                                         select A).ToList().Where(p => p.JoinDate >= fromDay && p.JoinDate <= toDay).ToList();
                }

                List<int> days = new List<int>();
                int i = DateTime.Now.Date.Day;
                int count = 0;
                while (i > 0 && count < 22)
                {
                    days.Add(i);
                    --i;
                    ++count;
                }

                days.Reverse();

                int month = DateTime.Now.Month;
                int year = DateTime.Now.Year;

                foreach (var day in days)
                {
                    DateTime dateFrom = new DateTime(year, month, day).Date + new TimeSpan(0, 0, 1);
                    DateTime dateTo = new DateTime(year, month, day).Date + new TimeSpan(23, 59, 59);
                    List<Order> orders = context.Orders.Where(p => p.DeliveryDate >= dateFrom && p.DeliveryDate <= dateTo).ToList();
                    Something a = new Something
                    {
                        key = day.ToString(),
                        value = orders.Count
                    };
                    countOrderList.Add(a);

                    Something b = new Something
                    {
                        key = day.ToString(),
                        value = Convert.ToInt64(orders.Sum(p => p.Total))
                    };
                    amountIncomeList.Add(b);
                }

                DateTime beginYesterday = DateTime.Now.AddDays(-1).Date + new TimeSpan(0, 0, 1);
                DateTime endYesterday = DateTime.Now.AddDays(-1).Date + new TimeSpan(23, 59, 59);

                long tempYesterday = Convert.ToInt64((from A in context.Orders
                                                      join B in context.OrderDetails
                                                      on A.ID equals B.OrderID
                                                      where A.DeliveryDate >= beginYesterday
                                                      && A.DeliveryDate <= endYesterday
                                                      select B).ToList().Sum(p => p.Quantity * p.PriceNow));
                long tempToday = Convert.ToInt64((from A in context.Orders
                                                  join B in context.OrderDetails
                                                  on A.ID equals B.OrderID
                                                  where A.DeliveryDate >= fromDay && A.DeliveryDate <= toDay
                                                  select B).ToList().Sum(p => p.Quantity * p.PriceNow));

                double increaseRate = tempToday == 0 ? 0 : ((double)tempToday / tempYesterday) - 1;

                return Json(new
                {
                    message = "success",
                    orderCount = orderList.Count.ToString(),
                    increaseRate = Math.Truncate(increaseRate * 100),
                    registerCount = memberAccountList.Count.ToString(),
                    statisticSum = orderList.Sum(p => p.Total).ToString(),
                    jsonCountData = countOrderList,
                    jsonIncomeData = amountIncomeList
                });
            }
        }

        [Route("month")]
        [HttpGet]
        public IHttpActionResult Month()
        {
            using (var context = new Context())
            {
                context.Configuration.ProxyCreationEnabled = false;
                DateTime fromDay = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                DateTime toDay = fromDay.AddMonths(1).AddDays(-1);

                List<Something> countOrderList = new List<Something>();
                List<Something> amountIncomeList = new List<Something>();

                List<Order> orderList = new List<Order>();
                List<MemberAccount> memberAccountList = new List<MemberAccount>();

                if (context.Orders.Any(p => p.DeliveryDate >= fromDay && p.DeliveryDate <= toDay))
                {
                    orderList = context.Orders.Where(p => p.DeliveryDate >= fromDay && p.DeliveryDate <= toDay).ToList();
                }
                if ((from A in context.MemberAccounts
                     join B in context.AccountTypes
                     on A.MemberTypeID equals B.ID
                     where B.UserTypeName != "Admin"
                     select A).Any(p => p.JoinDate >= fromDay && p.JoinDate <= toDay))
                {
                    memberAccountList = (from A in context.MemberAccounts
                                         join B in context.AccountTypes
                                         on A.MemberTypeID equals B.ID
                                         where B.UserTypeName != "Admin"
                                         select A).ToList().Where(p => p.JoinDate >= fromDay && p.JoinDate <= toDay).ToList();
                }
                Dictionary<int, int> valuePairs = new Dictionary<int, int>();
                valuePairs.Add(1, 5);
                valuePairs.Add(6, 9);
                valuePairs.Add(10, 15);
                valuePairs.Add(16, 19);
                valuePairs.Add(20, 25);
                valuePairs.Add(26, toDay.Day);

                foreach (var item in valuePairs)
                {
                    List<Order> orders = GetListOrderByMonthCondition(orderList, item.Key, item.Value);
                    Something i = new Something
                    {
                        key = (item.Key + "-" + item.Value).ToString(),
                        value = orders.Count
                    };
                    countOrderList.Add(i);

                    Something j = new Something
                    {
                        key = (item.Key + "-" + item.Value).ToString(),
                        value = Convert.ToInt64(orders.Sum(p => p.Total))
                    };
                    amountIncomeList.Add(j);
                }

                return Json(new
                {
                    message = "success",
                    orderCount = orderList.Count.ToString(),
                    increaseRate = "NaN",
                    registerCount = memberAccountList.Count.ToString(),
                    statisticSum = orderList.Sum(p => p.Total).ToString(),
                    jsonCountData = countOrderList,
                    jsonIncomeData = amountIncomeList
                });
            }
        }

        [Route("year")]
        [HttpGet]
        public IHttpActionResult Year()
        {
            using (var context = new Context())
            {
                context.Configuration.ProxyCreationEnabled = false;
                DateTime fromDay = new DateTime(DateTime.Now.Year, 1, 1) + new TimeSpan(0, 0, 1);
                DateTime toDay = new DateTime(DateTime.Now.Year, 12, 31) + new TimeSpan(23, 59, 59);

                List<Something> countOrderList = new List<Something>();
                List<Something> amountIncomeList = new List<Something>();

                List<Order> orderList = new List<Order>();
                List<MemberAccount> memberAccountList = new List<MemberAccount>();

                if (context.Orders.Any(p => p.DeliveryDate >= fromDay && p.DeliveryDate <= toDay))
                {
                    orderList = context.Orders.Where(p => p.DeliveryDate >= fromDay && p.DeliveryDate <= toDay).ToList();
                }
                if ((from A in context.MemberAccounts
                     join B in context.AccountTypes
                     on A.MemberTypeID equals B.ID
                     where B.UserTypeName != "Admin"
                     select A).Any(p => p.JoinDate >= fromDay && p.JoinDate <= toDay))
                {
                    memberAccountList = (from A in context.MemberAccounts
                                         join B in context.AccountTypes
                                         on A.MemberTypeID equals B.ID
                                         where B.UserTypeName != "Admin"
                                         select A).ToList().Where(p => p.JoinDate >= fromDay && p.JoinDate <= toDay).ToList();
                }

                Dictionary<int, int> valuePairs = new Dictionary<int, int>();
                valuePairs.Add(1, 2);
                valuePairs.Add(3, 4);
                valuePairs.Add(5, 6);
                valuePairs.Add(7, 8);
                valuePairs.Add(9, 10);
                valuePairs.Add(11, 12);

                foreach (var item in valuePairs)
                {
                    List<Order> orders = GetListOrderByYearCondition(orderList, item.Key, item.Value);
                    List<MemberAccount> members = GetListMemberByYearCondition(memberAccountList, item.Key, item.Value);
                    Something i = new Something
                    {
                        key = (item.Key + "-" + item.Value).ToString(),
                        value = orders.Count
                    };
                    countOrderList.Add(i);

                    Something j = new Something
                    {
                        key = (item.Key + "-" + item.Value).ToString(),
                        value = Convert.ToInt64(orders.Sum(p => p.Total))
                    };
                    amountIncomeList.Add(j);

                }

                return Json(new
                {
                    message = "success",
                    orderCount = orderList.Count.ToString(),
                    increaseRate = "NaN",
                    registerCount = memberAccountList.Count.ToString(),
                    statisticSum = orderList.Sum(p => p.Total).ToString(),
                    jsonCountData = countOrderList,
                    jsonIncomeData = amountIncomeList
                });
            }
        }

        [Route("all")]
        [HttpGet]
        public IHttpActionResult All()
        {
            using (var context = new Context())
            {
                context.Configuration.ProxyCreationEnabled = false;
                List<Order> orderList = context.Orders.ToList();
                List<MemberAccount> memberAccountList = (from A in context.MemberAccounts
                                                         join B in context.AccountTypes
                                                         on A.MemberTypeID equals B.ID
                                                         where B.UserTypeName != "Admin"
                                                         select A).ToList();

                List<Something> countOrderList = new List<Something>();
                List<Something> amountIncomeList = new List<Something>();
                for (int i = 0; i < 12; ++i)
                {
                    DateTime fromDay = DateTime.Parse("01/01/" + (DateTime.Now.Year + i));
                    DateTime toDay = DateTime.Parse("31/12/" + (DateTime.Now.Year + i));
                    string tomorrow = (DateTime.Now.Year + i).ToString();
                    if (context.Orders.Any(p => p.DeliveryDate >= fromDay && p.DeliveryDate <= toDay))
                    {
                        List<Order> orders = context.Orders.Where(p => p.DeliveryDate >= fromDay && p.DeliveryDate <= toDay).ToList();
                        Something item = new Something
                        {
                            key = tomorrow,
                            value = orders.Count
                        };
                        countOrderList.Add(item);

                        Something item2 = new Something
                        {
                            key = tomorrow,
                            value = Convert.ToInt64(orders.Sum(p => p.Total))
                        };
                        amountIncomeList.Add(item2);
                    }
                    else
                    {
                        Something item = new Something
                        {
                            key = tomorrow,
                            value = 0
                        };
                        countOrderList.Add(item);

                        Something item2 = new Something
                        {
                            key = tomorrow,
                            value = 0
                        };
                        amountIncomeList.Add(item2);
                    }
                }

                return Json(new
                {
                    message = "success",
                    orderCount = orderList.Count.ToString(),
                    increaseRate = "NaN",
                    registerCount = memberAccountList.Count.ToString(),
                    statisticSum = orderList.Sum(p => p.Total).ToString(),
                    jsonCountData = countOrderList,
                    jsonIncomeData = amountIncomeList
                });
            }
        }

        private List<Order> GetListOrderByHourCondition(List<Order> orderList, int hourFrom, int hourTo)
        {
            DateTime from = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day) + new TimeSpan(hourFrom, 0, 1);
            DateTime to = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day) + new TimeSpan(hourTo, 59, 59);
            List<Order> orders = orderList.Where(p => p.DeliveryDate >= from && p.DeliveryDate <= to).ToList();
            return orders;
        }

        private List<Order> GetListOrderByMonthCondition(List<Order> orderList, int dayFrom, int dayTo)
        {
            DateTime from = new DateTime(DateTime.Now.Year, DateTime.Now.Month, dayFrom).Date + new TimeSpan(0, 0, 1);
            DateTime to = new DateTime(DateTime.Now.Year, DateTime.Now.Month, dayTo) + new TimeSpan(23, 59, 59);
            List<Order> orders = orderList.Where(p => p.DeliveryDate >= from && p.DeliveryDate <= to).ToList();
            return orders;
        }

        private List<Order> GetListOrderByYearCondition(List<Order> orderList, int monthFrom, int monthTo)
        {
            DateTime from = new DateTime(DateTime.Now.Year, monthFrom, 1).Date + new TimeSpan(0, 0, 1);
            DateTime to = new DateTime(DateTime.Now.Year, monthTo, DateTime.DaysInMonth(DateTime.Now.Year, monthTo)) + new TimeSpan(23, 59, 59);
            List<Order> orders = orderList.Where(p => p.DeliveryDate >= from && p.DeliveryDate <= to).ToList();
            return orders;
        }

        private List<MemberAccount> GetListMemberByHourCondition(List<MemberAccount> members, int hourFrom, int hourTo)
        {
            DateTime from = DateTime.Now.Date + new TimeSpan(hourFrom, 0, 0);
            DateTime to = DateTime.Now.Date + new TimeSpan(hourTo, 59, 59);
            List<MemberAccount> memberAccounts = members.Where(p => p.JoinDate >= from && p.JoinDate <= to).ToList();
            return memberAccounts;
        }

        private List<MemberAccount> GetListMemberByMonthCondition(List<MemberAccount> members, int dayFrom, int dayTo)
        {
            DateTime from = new DateTime(DateTime.Now.Year, DateTime.Now.Month, dayFrom).Date + new TimeSpan(0, 0, 1);
            DateTime to = new DateTime(DateTime.Now.Year, DateTime.Now.Month, dayTo).Date + new TimeSpan(23, 59, 59);
            List<MemberAccount> memberAccounts = members.Where(p => p.JoinDate >= from && p.JoinDate <= to).ToList();
            return memberAccounts;
        }

        private List<MemberAccount> GetListMemberByYearCondition(List<MemberAccount> members, int monthFrom, int monthTo)
        {
            DateTime from = new DateTime(DateTime.Now.Year, DateTime.Now.Month, monthFrom).Date + new TimeSpan(0, 0, 1);
            DateTime to = new DateTime(DateTime.Now.Year, monthTo, DateTime.DaysInMonth(DateTime.Now.Year, monthTo)) + new TimeSpan(23, 59, 59);
            List<MemberAccount> memberAccounts = members.Where(p => p.JoinDate >= from && p.JoinDate <= to).ToList();
            return memberAccounts;
        }
    }
}