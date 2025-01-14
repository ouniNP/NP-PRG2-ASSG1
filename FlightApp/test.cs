class Shape //class header
{
	public string type {get; set;}
	public double length{get; set;}
	
	public Shape(){} //default constructor
	
	public Shape(string t,double l) //parameterised constructor
	{
		type = t;
		length = l;
	}
	
	public override string ToString()
	{
		return "";
	}
}