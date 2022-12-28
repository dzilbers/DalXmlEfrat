namespace Dal;
using DO;

///////////////////////////////////////////
//implement ILecturer with XML Serializer
//////////////////////////////////////////
class Lecturer : DalApi.ILecturer
{
    const string s_lecturers = "lecturers"; //XML Serializer

    public IEnumerable<DO.Lecturer?> GetAll(Func<DO.Lecturer?, bool>? filter = null)
    {
        var listLecturers = (List<DO.Lecturer?>)XMLTools.LoadListFromXMLSerializer<DO.Lecturer?>(s_lecturers)!;

        if (filter == null)
            return listLecturers.Select(p => p).OrderBy(lec => ((DO.Lecturer)lec!).ID);
        else
            return listLecturers.Where(filter).OrderBy(lec => ((DO.Lecturer)lec!).ID);
    }

    public DO.Lecturer GetById(int id)
    {
        List<DO.Lecturer> listLecturers = XMLTools.LoadListFromXMLSerializer<DO.Lecturer>(s_lecturers);

        if (listLecturers.Exists(p => p.ID == id))
            return listLecturers.FirstOrDefault(p => p.ID == id);
        else
            throw new Exception("missing id");//DalMissingIdException(id, "Lecturer");
    }
    public int Add(DO.Lecturer lecturer)
    {
        List<DO.Lecturer> listLecturers = XMLTools.LoadListFromXMLSerializer<DO.Lecturer>(s_lecturers);

        if (listLecturers.Exists(lec => lec.ID == lecturer.ID))
            throw new Exception("id already exist");//DalAlreadyExistIdException(lecturer.ID, "Lecturer");

        listLecturers.Add(lecturer);

        XMLTools.SaveListToXMLSerializer(listLecturers, s_lecturers);

        return lecturer.ID;
    }

    public void Delete(int id)
    {
        List<DO.Lecturer> listLecturers = XMLTools.LoadListFromXMLSerializer<DO.Lecturer>(s_lecturers);

        if (listLecturers.RemoveAll(p => p.ID == id) == 0)
            throw new Exception("missing id"); //new DalMissingIdException(id, "Lecturer");

        XMLTools.SaveListToXMLSerializer(listLecturers, s_lecturers);
    }
    public void Update(DO.Lecturer lecturer)
    {
        Delete(lecturer.ID);
        Add(lecturer);
    }
}
