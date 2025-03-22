namespace ContainersApp.Exceptions;

public class ShipLoadLimitReachedException(string message = "Maximum load of ship is exceeded") : Exception(message);
