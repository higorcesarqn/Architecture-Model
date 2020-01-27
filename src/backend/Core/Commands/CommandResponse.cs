namespace Core.Commands
{
    public class CommandResponse
    {
        public static CommandResponse Ok = new CommandResponse { Success = true };
        public static CommandResponse Fail = new CommandResponse { Success = false };

        public CommandResponse(bool success = false)
        {
            Success = success;
        }

        public bool Success { get; private set; }

        public static implicit operator bool(CommandResponse commandResponse)
            => commandResponse.Success;


        public static implicit operator CommandResponse(bool value)
            => new CommandResponse(value);

    }
}