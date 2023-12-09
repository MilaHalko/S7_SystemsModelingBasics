public class Create {
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
