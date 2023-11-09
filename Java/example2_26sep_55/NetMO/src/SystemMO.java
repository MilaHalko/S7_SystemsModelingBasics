import java.util.logging.Logger;

public class SystemMO {
    @Override
    public int getState() {
        return d.getState() + qu.getState();
    }

    @Override
    public void in(double t) {
        if (d.getIsEmpty) {
            d.in(t);
            super.setTimeNextEvent(this.getDevices().getTimeNextEvent());
        } else {
            if (qu == null) super.increaseOutUnserv();
            else {
                if (qu.isEmpty()) qu.seize();
                else super.increaseUnserv();
            }
        }
    }

    @Override
    public void out(double t) {
        try {
            d.out(t);
            this.increaseServ();
            this.setTimeNextEvent(this.getDevices().getTimeNextEvent());

            if (qu != null && qu.getState() > 0) {
                qu.release();
                d.in(t);
                this.setTimeNextEvent(this.getDevices().getTimeNextEvent());
            }
            if (this.getNet() != null) this.getNet().in(t);
        }
        catch (NoException ex) {
            Logger.getLogger(SystemMO.class.getName()).log(Level.SEVERE, null, ex);
        }
    }
}
}
