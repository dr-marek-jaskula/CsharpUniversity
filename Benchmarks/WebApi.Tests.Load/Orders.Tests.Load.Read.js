import http from 'k6/http';
import { check, group, sleep } from 'k6';
import { Counter, Trend } from 'k6/metrics';

// Parameters & Constants
const BASE_URL = 'https://localhost:7240';
const DEBUG = true;

// Counters
var ReadCounter = new Counter('ReadCounter');

// Trends
var ReadTrend = new Trend('ReadTrend');

const ExecutionType = 
{
    load:           'load',
    smoke:          'smoke',
    stress:         'stress',
    soak:           'soak',
    spike:          'spike',
    performance:    'performance'
}

var ExecutionScenarios;
//To execute a different test scenario: change the Exectution variable to one of the ExecutionTypes that are specified above
var Execution = 'performance';

switch(Execution)
{
	case ExecutionType.smoke:
		ExecutionScenarios = 
        {
			ReadScenario: 
            {
                exec: 'ReadTests',
                executor: 'ramping-arrival-rate', //If you need your tests to not be affected by the system-under-test's performance, and would like to ramp the number of iterations up or down during specific periods of time.
                startTime: '0s', //Time offset since the start of the test, at which point this scenario should begin execution.
                startRate: 1, //Number of iterations to execute each second (timeUnit is "1s" by default)
                preAllocatedVUs: 4, //Number of VUs to pre-allocate before test start to preserve runtime resources.
                stages: //Array of objects that specify the target number of iterations to ramp up or down to.
                [
                    { duration: '30s', target: 1 },
                    { duration: '30s', target: 2 }
                ]
            }
        }; 
        break;
    case ExecutionType.load:
        ExecutionScenarios = 
        {
            ReadScenario: 
            {
                exec: 'ReadTests',
                executor: 'ramping-arrival-rate',
                startTime: '0s',
                startRate: 1,
                preAllocatedVUs: 4,
                stages: 
                [
                    { duration: '30s', target: 2 },
                    { duration: '45m', target: 2 }
                ]
            }
        }; 
        break;  
    case ExecutionType.stress:
        ExecutionScenarios = 
        {
            ReadScenario: 
            {
                exec: 'ReadTests',
                executor: 'ramping-arrival-rate',
                startTime: '0s',
                startRate: 10,
                preAllocatedVUs: 160,
                stages: 
                [
                    { duration: '5m', target: 10 },
                    { duration: '5m', target: 20 },
                    { duration: '5m', target: 40 },
                    { duration: '5m', target: 80 }
                ]
            }
        }; 
        break;  
    case ExecutionType.soak:
        ExecutionScenarios = 
        {
            ReadScenario: 
            {
                exec: 'ReadTests',
                executor: 'ramping-arrival-rate',
                startTime: '0s',
                startRate: 1,
                preAllocatedVUs: 40,
                stages: 
                [
                    { duration: '5m', target: 5 },
                    { duration: '5m', target: 10 },
                    { duration: '8h', target: 20 }
                ]
            }
        }; 
        break;  
    case ExecutionType.spike:
        ExecutionScenarios = 
        {
            ReadScenario: 
            {
                exec: 'ReadTests',
                executor: 'ramping-arrival-rate',
                startTime: '0s',
                startRate: 1,
                preAllocatedVUs: 0,
                stages: 
                [
                    { duration: '10s', target: 100 },
                    { duration: '1m', target: 100 },
                    { duration: '10s', target: 1400 },
                    { duration: '3m', target: 1400 },
                    { duration: '10s', target: 100 },
                    { duration: '3m', target: 100 },
                    { duration: '10s', target: 0 }
                ]
            }
        }; 
        break;           
    case ExecutionType.performance:
        ExecutionScenarios = 
        {
            ReadScenario: 
            {
                exec: 'ReadTests',
                executor: 'ramping-arrival-rate',
                startTime: '0s',
                startRate: 1,
                preAllocatedVUs: 20,
                stages: 
                [
                    { duration: '10s', target: 10 },
                    { duration: '1m', target: 10 },
                    { duration: '10s', target: 0 }
                ]
            }
        }
        break;        
}

export let options =
{
    scenarios: ExecutionScenarios,
    thresholds: 
    {
        http_req_failed: ['rate<0.05'],   
        'http_req_duration': ['p(95)<500', 'p(99)<1500'],
        'http_req_duration{name:Read}': ['avg<500', 'max<1000']
    }
};  

function GetRandom(min, max) 
{
    min = Math.ceil(min);
    max = Math.floor(max);
    return Math.floor(Math.random() * (max - min + 1)) + min;
}  

function FormatDate(date) 
{
    var hours = date.getHours();
    var minutes = date.getMinutes();
    var ampm = hours >= 12 ? 'pm' : 'am';
    hours = hours % 12;
    hours = hours ? hours : 12; // the hour '0' should be '12'
    minutes = minutes < 10 ? '0'+ minutes : minutes;
    var strTime = hours + ':' + minutes + ' ' + ampm;
    return (date.getMonth()+1) + "/" + date.getDate() + "/" + date.getFullYear() + "  " + strTime;
}

function LogInDebugMode(textToLog)
{
    if (DEBUG)
    {
        console.log(`${textToLog}`); 
    }
}

// Testing WebApi reading data
export function ReadTests()
{
    var orderId = GetRandom(1, 6);

    let response = http.get
    (
        `${BASE_URL}/api/Order/Anonymous/${orderId}`,
        { tags: { name: 'Read' } }
    );

    const isSuccessfulRequest = check(response, 
    {
        "Order request succeed": () => response.status == 200 //Ok
    });

    if (isSuccessfulRequest)
    {
        LogInDebugMode(`response.body: ${response.body}`)
        ReadTrend.add(response.timings.duration);
        ReadCounter.add(1);
        let body = JSON.parse(response.body);
        var amount = body.amount; 

        check(amount, 
        {
            'Order needs to have at least amount of one': Math.min(amount) > 0
        });
    }  
}    

// setup configuration
export function setup() 
{
    LogInDebugMode(`==========================SETUP BEGINS==========================`)
    // log the date & time start of the test
    LogInDebugMode(`Start of test: ${FormatDate(new Date())}`)

    // log the test type
    LogInDebugMode(`Test executed: ${Execution}`)

    LogInDebugMode(`==========================SETUP ENDS==========================`)
}