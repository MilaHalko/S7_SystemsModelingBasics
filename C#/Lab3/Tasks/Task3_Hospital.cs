// using MassServiceModeling.Elements;
// using MassServiceModeling.Enums;
//
// namespace Lab3;
//
// public class Task3_Hospital : Task
// {
//     public Task3_Hospital()
//     {
//         // TODO: Different types of created :                             1) examined -> ward   2) not examined -> ward   3) not examined
//         // ASK: відносна частота ... 0.5, 0.1, 0.4
//         // ASK: Приймальне відділення та реєстраціє одне і теж?
//         // TODO: math expectation for Create = 15
//         Create patients = new(15);
//
//         Process registration_examined = new(15);
//         Process registration_partlyExamined = new(40);
//         Process registration_notExamined = new(30);
//
//         // TODO: Priority of Create type for doctors:                     1) examined   2) partly examined   3) not examined
//         Process doctors = new(2, 2);
//
//         // TODO: Create Type goes to specific nextElement :               When in reception 1) registration_examined -> attendants     2) other -> laboratory 
//         // TODO: Uniform from 3 to 8
//         Process attendants = new(5, subProcessCount: 3, distribution: Distribution.Uniform);
//
//         // TODO: Uniform from 2 to 5
//         Process transitionReceptionLab = new(2, Int32.MaxValue);
//
//         // TODO: Erlang with math expectation = 4.5 and k = 3
//         Process labRegistry = new(2);
//
//         // TODO: Erlang with math expectation = 4 and k = 2
//         Process labAssistants = new(2, 2);
//
//         // ASK: Після здачі аналізів хворі або повертаються в приймальне відділення (якщо їх приймають у лікарню), або залишають лікарню (якщо їм було призначено тільки попереднє обстеження).
//         // TODO: after labAssistants goes to specific nextElement :       1) became type #1 -> reception 
//
//         patients.SetNextElement(registration_examined);
//         patients.SetNextElement(registration_partlyExamined);
//         patients.SetNextElement(registration_notExamined);
//
//         registration_examined.SetNextElement(doctors);
//         registration_partlyExamined.SetNextElement(doctors);
//         registration_notExamined.SetNextElement(doctors);
//
//         doctors.SetNextElement(attendants);
//         doctors.SetNextElement(transitionReceptionLab);
//         
//         transitionReceptionLab.SetNextElement(labRegistry);
//         transitionReceptionLab.SetNextElement(registration_examined);
//         
//         labRegistry.SetNextElement(labAssistants);
//         labAssistants.SetNextElement(transitionReceptionLab);
//         
//         model = new(new List<Element>()
//         {
//             patients, registration_examined, registration_partlyExamined, registration_notExamined, doctors, attendants,
//             transitionReceptionLab, labRegistry, labAssistants
//         });
//     }
// }