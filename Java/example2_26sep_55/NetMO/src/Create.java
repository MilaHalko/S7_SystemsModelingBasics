package NetMO;
public class Create extends Element {
    public Create() {
        super();
        super.setTimeNextEvent(0.0);
        super.setName("CREATOR");
    }

    public Create (double t) {
        super();
        super.setTimeNextEvent(t);
    }
    public Create(double tCreate, double param) {
        super.setTimeNextEvent(0.0);
        super.setTimeDelayMean(tCreate);
        super.setParametr(param);
    }

    @Override
    public void out(double t) {
        getNextElement().in(t);
        super.setTimeNextEvent(t + super.getTimeDelay());
    }
}
