import java.util.Random;

public class FunRand {

    private static double getRandom() {
        double a = 0;
        while (a == 0) {
            a = Math.random();
        }
        return a;
    }

    public static double Exponential(double timeMean) {
        System.out.println("  Delay = " + (-timeMean * Math.log(getRandom())) + "\n");
        return -timeMean * Math.log(getRandom());
    }

    public static double Uniform(double timeMin, double timeMax) throws ExceptionInvalidTimeDelay {
        return ValidateDelay(timeMin + getRandom() * (timeMax - timeMin));
    }

    public static double Normal(double timeMean, double timeDeviation) throws ExceptionInvalidTimeDelay {
        Random random = new Random();
        return ValidateDelay(timeMean + timeDeviation * random.nextGaussian());
    }

    public static double Empiric(double[] x, double[] y) throws Exception {
        int l = x.length;
        if (y[l - 1] != 1.0) throw new Exception("Wrong array for Empiric distribution!");
        double r = Math.random();

        for (int i = 1; i < l - 1; i++) {
            if (r > y[i - 1] && r <= y[i])
                return x[i - 1] + (r - y[i - 1]) * (x[i] - x[i - 1]) / (y[i] - y[i - 1]);
        }
        return ValidateDelay((x[l-2]+(r-y[l-2]))*(x[l-1]-x[l-2])/(y[l-1]-y[l-2]));
    }

    private static double ValidateDelay(double delay) throws ExceptionInvalidTimeDelay {
        if (delay < 0) throw new ExceptionInvalidTimeDelay("Negative time delay is generated!");
        return delay;
    }
}
