public class NoComponentException : System.Exception {
  public NoComponentException() { }
  public NoComponentException(string message) : base(message) { }
  public NoComponentException(string message, System.Exception inner) : base(message, inner) { }
  protected NoComponentException(
    System.Runtime.Serialization.SerializationInfo info,
    System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}