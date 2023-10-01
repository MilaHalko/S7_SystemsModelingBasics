public abstract class Element {
    private double tnext, tcurr;
    private double delayMean, delayDev;
    private int state;
    private int quantity;

    private Element nextElement;
    private int id;
    private static int nextId = 0;
    private String name;
    private String distribution;

    public Element() {
        tnext = Double.MAX_VALUE;
        tcurr = tnext;
        delayMean = 1.0;
        state = 0;

        nextElement = null;
        id = nextId;
        nextId++;
        name = "element" + id;
        distribution = "exp";
    }

    public Element(double delay) {
        tnext = 0.0;
        tcurr = tnext;
        delayMean = delay;
        state = 0;

        nextElement = null;
        id = nextId;
        nextId++;
        name = "element" + id;
        distribution = "";
    }

    public double getTnext() {return tnext;}
    public void setTnext(double tnext) {this.tnext = tnext;}

    public double getTcurr() {return tcurr;}
    public void setTcurr(double tcurr) {this.tcurr = tcurr;}

    public double getDelay() throws ExceptionInvalidTimeDelay {
        double delay = getDelayMean();
        if ("exp".equalsIgnoreCase(getDistribution())) {
            delay = FunRand.Exponential(getDelayMean());
        } else {
            if ("norm".equalsIgnoreCase(getDistribution())) {
                delay = FunRand.Normal(getDelayMean(),
                        getDelayDev());
            } else {
                if ("unif".equalsIgnoreCase(getDistribution())) {
                    delay = FunRand.Uniform(getDelayMean(),
                            getDelayDev());
                } else {
                    if("".equalsIgnoreCase(getDistribution()))
                        delay = getDelayMean();
                }
            }
        }
        return delay;
    }

    public double getDelayMean() {return delayMean;}
    public void setDelayMean(double delayMean) {this.delayMean = delayMean;}

    public double getDelayDev() {return delayDev;}
    public void setDelayDev(double delay) {delayDev = delay;}

    public int getState() {return state;}
    public void setState(int state) {this.state = state;}

    public int getQuantity() {return quantity;}

    public Element getNextElement() {return nextElement;}
    public void setNextElement(Element nextElement) {this.nextElement = nextElement;}

    public int getId() {return id;}
    public void setId(int id) {this.id = id;}

    public String getName() {return name;}
    public void setName(String name) {this.name = name;}

    public String getDistribution() {return distribution;}
    public void setDistribution(String distribution) {this.distribution = distribution;}

    public void outAct() {
        quantity++;
    }

    public void printResult(){
        System.out.println(getName() + " quantity = " + quantity);
    }

    public void printInfo(){
        System.out.println(getName()+ " state= " + state + " quantity = " + quantity + " tnext= "+ tnext);
    }

    public abstract void inAct() throws ExceptionInvalidTimeDelay;
    public abstract void doStatistics(double delta);
}
