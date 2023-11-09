import javax.lang.model.element.Element;
import java.util.ArrayList;

public class Main {
    public static void main(String[] args) {
        NetMO model = getModelFork();
        model.setTmod(1000);
        model.go();
        for (SystemMO smo : model.getSystems())
            System.out.println("\nunserved " + smo.getOutUnserv() + " served " + smo.getOutServ() + " final state " + smo.getState());
        System.out.println("\n--RESULTS--");
        for (SystemMO smo : model.getSystems())
            System.out.println("Failure probability " + smo.getOutUnserv() * 1.0 / (smo.getOutServ() + smo.getOutUnserv()));
        for (SystemMO smo : model.getSystems())
            System.out.println("mean queue " + m/model.getTmod());
    }

    public static NetMO getModel() {
        Process[] devices1 = new Process[2], devices2 = new Process[1];
        for (int j = 0; j < devices1.length; j++)
            devices1[j] = new Process(4.0, 1.0);
        for (int j = 0; j < devices2.length; j++)
            devices2[j] = new Process(4.0, 1.0);

        SystemMO SMO1 = new SystemMO(new MultiProcess(devices1, new QueueWithMax(0, 3)));
        SMO1.setName("SMO1");
        SystemMO SMO2 = new SystemMO(new MultiProcess(devices2, new QueueWithMax(0, 2)));
        SMO2.setName("SMO2");

        ArrayList<SystemMO> arraySMO = new ArrayList<>();
        arraySMO.add(SMO1);
        arraySMO.add(SMO2);
        Create g = new Create(2.0, 1.0);
        g.setName("CREATOR");

        ArrayList<Create> arrayC = new ArrayList<>();
        arrayC.add(g);

        g.setNextElement(SMO1);
        SMO1.setNextElement(SMO2);

        return new NetMO(arraySMO, arrayC);
    }

    public static NetMO getModelFork() {
        Process[] devices1 = new Process[2], devices2 = new Process[1];
        for (int j = 0; j < devices1.length; j++)
            devices1[j] = new Process(4.0, 1.0);
        for (int j = 0; j < devices2.length; j++)
            devices2[j] = new Process(4.0, 1.0);

        SystemMO SMO1 = new SystemMO(new MultiProcess(devices1, new QueueWithMax(0, 3)));
        SMO1.SetName("SMO1");
        SystemMO SMO2 = new SystemMO(new MultiProcess(devices2, new QueueWithMax(0, 2)));
        SMO2.SetName("SMO2");

        ArrayList<SystemMO> arraySMO = new ArrayList<>();
        arraySMO.add(SMO1);
        arraySMO.add(SMO2);
        Create g = new Create(2.0, 1.0);
        g.SetName("CREATOR");

        ArrayList<Create> arrayC = new ArrayList<>();
        arrayC.add(g);

        // розгалудження маршруту
        Element[] elements = {SMO1, SMO2};
        double[] pp = {0.5, 0.5};

        g.setNextElement(elements, pp);
        SMO1.setNextElement(SMO2);

        return new NetMO(arraySMO, arrayC);
    }
}