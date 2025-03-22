using ContainersApp.Containers;
using ContainersApp.Exceptions;

namespace ContainersApp;

public class ContainerShip
{
    private int _maxSpeed;
    private List<Container> _containers = new List<Container>();
    private int _maxNumOfCont;
    private double _maxWeight;


    public ContainerShip(int maxSpeed, int maxNumOfCont, double maxHeight, List<Container> passedContainers)
    { 
        _maxSpeed = maxSpeed;
        _maxNumOfCont = maxNumOfCont;
        _maxWeight = maxHeight;
        if (passedContainers.Count>_maxNumOfCont)
        {
            throw new ShipLoadLimitReachedException();
        }

        if (SumOfWeight(passedContainers)>_maxWeight)
        {
            throw new ShipMaxWeightReachedException();
        }
        Containers = passedContainers;
        
    }

    public List<Container> Containers{ get;}

    public void LoadContainer(Container container)
    {
        if (_containers.Count+1 > _maxNumOfCont)
        {
            throw new ShipLoadLimitReachedException();
        }
        if (SumOfWeight(_containers)+container.TareWeight+container.CargoMass>_maxWeight)
        {
            throw new ShipMaxWeightReachedException();
        }
        _containers.Add(container);
    }

    public void LoadContainers(List<Container> passedContainers)
    {
        if (passedContainers.Count+_containers.Count>_maxNumOfCont)
        {
            throw new ShipLoadLimitReachedException();
        }
        if (SumOfWeight(passedContainers)+SumOfWeight(_containers)>_maxWeight)
        {
            throw new ShipMaxWeightReachedException();
        }
        _containers.AddRange(passedContainers);
    }

    public void UnloadContainer(Container container)
    {
        _containers.Remove(container);
    }

    public void ReplaceContainer(Container container, int index)
    {
        if (index > _maxNumOfCont-1)
        {
            throw new ArgumentException();
        }
        List<Container> passedContainers = _containers;
        passedContainers.Insert(index,container);
        if (SumOfWeight(passedContainers)>_maxWeight)
        {
            throw new ShipMaxWeightReachedException();
        }
        _containers.Insert(index, container);
    }

    public void TransferContainerToShip(ContainerShip ship , int index)
    {
        
        Container container = ship.Containers[index];
        ship.Containers.Remove(container);
        if (_containers.Count + 1> _maxNumOfCont)
        {
            throw new ShipLoadLimitReachedException();
        }
        if (SumOfWeight(_containers)+container.TareWeight+container.CargoMass>_maxWeight)
        {
            throw new ShipMaxWeightReachedException();
        }
        Containers.Add(container);
    }

    public String PrintContainers()
    {
        String output = "";
        for (int i = 0; i > _containers.Count; i++)
        {
            output+=" "+_containers.ElementAt(i);
        }
        return output;
    }

    public double SumOfWeight(List<Container> passedContainers)
    {
       double sum = 0;
        for (int i = 0; i >passedContainers.Count; i++)
        {
            sum += passedContainers.ElementAt(i).CargoMass + passedContainers.ElementAt(i).TareWeight;
        }      
        return sum;
    }

    public override string ToString()
    {
        return "Ship "+"Maximum number of containers is "+_maxNumOfCont+" Maximum speed of ship is " + _maxSpeed 
               + " Maximum weight of containers is " + _maxWeight + " Containers" + PrintContainers();
    }
}