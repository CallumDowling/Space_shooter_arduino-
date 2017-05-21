
//Sketch to control the input for the joystick and buttons. 
//Input pins
int buttonPin = 2; //joystick press
int buttonPin2 = 3; //white button
int buttonPin3 = 4; // red button
int buttonPin4 = 7; // bluebutton
int xAxisPin = 14;  //Joysick x
int yAxisPin = 15; // Joystick y
int zAxisPin = 16; //Altitude potentiometer

//Ints to store offset value from calibration
int xAxisOffset = 0;
int yAxisOffset = 0;

void setup() {

pinMode(buttonPin, INPUT_PULLUP);//joy button
pinMode(buttonPin2, INPUT_PULLUP);//white button
pinMode(buttonPin3, INPUT_PULLUP);//red button
pinMode(buttonPin4, INPUT_PULLUP);//bluebutton

pinMode(xAxisPin, INPUT);
pinMode(yAxisPin, INPUT);
pinMode(zAxisPin, INPUT);

//Calibrating the controller, if this is not done the joystick doesn't work properly
xAxisOffset = 512 - analogRead(xAxisPin);
yAxisOffset = 512 - analogRead(yAxisPin);
Serial.begin(9600);
}

void loop() {
  //Prints input values to port
  
  Serial.flush();
  Serial.print('h'); //Header for serial port
  Serial.print(analogRead(xAxisPin)+ xAxisOffset);
  Serial.print(",");
  Serial.print(analogRead(yAxisPin)+ yAxisOffset);
  Serial.print(",");
  if (digitalRead(buttonPin)==LOW){
    Serial.print(1);
  }
  else{
    Serial.print(0);
  }
  
  Serial.print(",");
  
  if (digitalRead(buttonPin2)==LOW){
    Serial.print(1);
  }
  else{
    Serial.print(0);
  }
  
  Serial.print(",");
  
  if (digitalRead(buttonPin3)==LOW){
    Serial.print(1);
    
  }
  else{
    Serial.print(0);
  }
  
  Serial.print(",");
  
  if (digitalRead(buttonPin4)==LOW){
    Serial.print(1);
    
  }
  else{
    Serial.print(0);
  }
  
  Serial.print(",");
  Serial.print(analogRead(zAxisPin));
  
  Serial.println();
  
  Serial.flush();
 
  //Adding a delay - because unity calls the update once per frame, and the frame rate is anywhere from 50-100 fps, a delay is required so inputs are not just put on the queue, introducing input lag.
  delay(10);
}
