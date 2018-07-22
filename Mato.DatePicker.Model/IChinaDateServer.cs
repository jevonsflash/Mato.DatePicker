using System;

namespace Mato.DatePicker.Model
{
    public interface IChinaDateServer
    {
        /// <summary>  

        /// 获取农历  

        /// </summary>  

        /// <param name="dt"></param>  

        /// <returns></returns>  
        string GetChinaDate(DateTime dt);

        /// <summary>  

        /// 获取农历年份  

        /// </summary>  

        /// <param name="dt"></param>  

        /// <returns></returns>  
        string GetYear(DateTime dt);

        /// <summary>  

        /// 获取农历月份  

        /// </summary>  

        /// <param name="dt"></param>  

        /// <returns></returns>  
        string GetMonth(DateTime dt);

        /// <summary>  

        /// 获取农历日期  

        /// </summary>  

        /// <param name="dt"></param>  

        /// <returns></returns>  
        string GetDay(DateTime dt);

        /// <summary>  

        /// 获取节气  

        /// </summary>  

        /// <param name="dt"></param>  

        /// <returns></returns>  
        string GetSolarTerm(DateTime dt);

        /// <summary>  

        /// 获取公历节日  

        /// </summary>  

        /// <param name="dt"></param>  

        /// <returns></returns>  
        string GetHoliday(DateTime dt);

        /// <summary>  

        /// 获取农历节日  

        /// </summary>  

        /// <param name="dt"></param>  

        /// <returns></returns>  
        string GetChinaHoliday(DateTime dt);

        /// <summary>  

        /// 阴历转为阳历  

        /// </summary>  

        /// <param name="year">指定的年份</param>  
        DateTime GetLunarYearDate(DateTime dt);

        /// <summary>  

        /// 阳历转为阴历  

        /// </summary>  

        /// <param name="dt">公历日期</param>  

        /// <returns>农历的日期</returns>  
        DateTime GetSunYearDate(DateTime dt);
    }
}
