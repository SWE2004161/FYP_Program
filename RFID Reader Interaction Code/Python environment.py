import serial
import tkinter as tk


# Establish serial connection
ser = serial.Serial('COM4', 9600)  # Replace 'COM4' with the appropriate port for your Arduino

# Create a Tkinter window
window = tk.Tk()

# Create a text widget to display the response
response_text = tk.Text(window)
response_text.pack()

def send_command():
    # Send commands to Arduino
    command = command_entry.get()  # Get the command from the text entry widget
    while len(command) < 64:  # If command length is less than 64, fill the rest with spaces
        command += ' '
    ser.write(("WRITE " + command +'\n').encode())  # Send the command as bytes
    window.after(100, read_response)

def read_block():
    ser.write(("READ"+ '\n').encode())  # Send the command as bytes
    window.after(100, read_response)

def read_response():
    while ser.in_waiting > 0:  # While there is data waiting in the serial buffer
        response = ser.readline().decode('ISO-8859-1', errors='replace').strip()  
        response_text.insert(tk.END, response + "\n")  # Add the response to the text widget
    
# Create text entry widget to input commands
command_entry = tk.Entry(window)
command_entry.pack()

# Create button to send command
send_button = tk.Button(window, text="Write to Block", command=send_command)
send_button.pack()

send_button = tk.Button(window, text="Read", command=read_block)
send_button.pack()

read_response()  # Start the initial call to read_response

# Run the Tkinter event loop
window.mainloop()

# Close the serial connection
ser.close()