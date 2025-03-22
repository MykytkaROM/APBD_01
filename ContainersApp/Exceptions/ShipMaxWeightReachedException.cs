namespace ContainersApp.Exceptions;

public class ShipMaxWeightReachedException(string message = "Maximum weight of containers ,that ship can transport is exceeded") : Exception(message);
