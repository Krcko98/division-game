using Grid.Data;

namespace Grid.Controller
{
    public class GridSlotKeepController : GridSlotController
    {
        public override void Init(GridSlotControllerData data)
        {
            base.Init(data);

            CanDrag = true;
        }
    }
}