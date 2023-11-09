public class Create extends Element {
    public Create(double delay) {
        super(delay);
        super.setTnext(0.0); // імітація розпочнеться з події Create
    }

    @Override
    public void outAct() throws ExceptionInvalidTimeDelay {
        super.outAct();
        super.setTnext(super.getTcurr() + super.getDelay());
        super.getNextElement().inAct();
    }

    @Override
    public void inAct() throws ExceptionInvalidTimeDelay {

    }

    @Override
    public void doStatistics(double delta) {

    }
}
