public class Element {
    public void setNextElement(Element nextElement) {
        this.nextElement[0] = nextElement;
    }

    public void setNextElement(Element[] nextElement, double[] probability) {
        this.nextElement = nextElement;
        this.probability = probability;
    }

    public void in(double t) {

    }

    public void out(double t) {
        outServ++;
    }
}
