Test_Student_LinqToXml();
Test_Lecturer_XmlSerializer();

static void Test_Student_LinqToXml()
{
    DalApi.IStudent dal = new Dal.Student();

    for (int i = 10; i > 0; --i)
        try
        {
            dal.Add(new()
            {
                ID = i,
                FirstName = "FN" + i,
                LastName = "LN" + i,
                StudentStatus = DO.StudentStatus.ACTIVE,
                BirthDate = DateTime.ParseExact("12.03.85", "dd.MM.yy", null),
                Grade = 100
            });
        }
        catch (Exception ex) { Console.WriteLine($"{ex.Message}\n{ex.InnerException}"); }

    try { Console.WriteLine(dal.GetById(1)); }
    catch (Exception ex) { Console.WriteLine($"{ex.Message}\n{ex.InnerException}"); }

    try { dal.Delete(5); }
    catch (Exception ex) { Console.WriteLine($"{ex.Message}\n{ex.InnerException}"); }

    try
    {
        dal.Update(new DO.Student
        {
            ID = 3,
            FirstName = "FNNew",
            //LastName = "LNNew", // empty (null) field
            //StudentStatus = DO.StudentStatus.FINISHED, // empty (null) field
            BirthDate = DateTime.ParseExact("15.05.55", "dd.MM.yy", null),
            Grade = 100
        });
    }
    catch (Exception ex) { Console.WriteLine($"{ex.Message}\n{ex.InnerException}"); }

    try { foreach (var item in dal.GetAll()) Console.WriteLine(item); }
    catch (Exception ex) { Console.WriteLine($"{ex.Message}\n{ex.InnerException}"); }
}

static void Test_Lecturer_XmlSerializer()
{
    DalApi.ILecturer dal = new Dal.Lecturer();

    for (int i = 10; i > 0; --i)
        try
        {
            dal.Add(new()
            {
                ID = i,
                FirstName = "FN" + i,
                LastName = "LN" + i,
                LecturerStatus = DO.LecturerStatus.SABBATICAL,
                SeniorStuff = true
            });
        }
        catch (Exception ex) { Console.WriteLine($"{ex.Message}\n{ex.InnerException}"); }

    try { Console.WriteLine(dal.GetById(1)); }
    catch (Exception ex) { Console.WriteLine($"{ex.Message}\n{ex.InnerException}"); }

    try { dal.Delete(5); }
    catch (Exception ex) { Console.WriteLine($"{ex.Message}\n{ex.InnerException}"); }

    try
    {
        dal.Update(new DO.Lecturer
        {
            ID = 3,
            FirstName = "FN3",
            //LastName = "LN3",
            //LecturerStatus = DO.LecturerStatus.ADJUNCT,
            SeniorStuff = false
        });
    }
    catch (Exception ex) { Console.WriteLine($"{ex.Message}\n{ex.InnerException}"); }

    try { foreach (var item in dal.GetAll()) Console.WriteLine(item); }
    catch (Exception ex) { Console.WriteLine($"{ex.Message}\n{ex.InnerException}"); }
}
