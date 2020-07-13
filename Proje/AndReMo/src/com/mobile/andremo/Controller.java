package com.mobile.andremo;

import java.net.*;
import java.util.Date;
import android.annotation.SuppressLint;
import android.app.Activity;
import android.content.Context;
import android.os.Bundle;
import android.view.KeyEvent;
import android.view.MotionEvent;
import android.view.View;
import android.view.View.OnKeyListener;
import android.view.View.OnTouchListener;
import android.view.inputmethod.InputMethodManager;
import android.widget.ImageView;
import android.widget.RelativeLayout;
import android.widget.TextView;
import android.widget.ToggleButton;

@SuppressLint("NewApi")
public class Controller extends Activity implements OnTouchListener {
    
	public DatagramPacket packet;
    public DatagramSocket socket;
    
    private float lastX;
    private float lastY;
    private float firstX;
    private float firstY;
    private Date clickDown;
    final int[] tolerance = {5,-5};
    
    public String GelenIp = null;
    public int tut=0;
    
    TextView tvControl;
    ImageView control;
    ToggleButton btnLeft,btnRight;
   
    private Bundle extras = null;
    protected View btnkeyb;
   
	@SuppressLint("NewApi")
	@Override
    public void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.controller);
        
        /*AdView adView = (AdView) this.findViewById(R.id.adView);
        AdRequest adRequest = new AdRequest.Builder().build();
        adView.loadAd(adRequest);*/
          
        tvControl=(TextView)findViewById(R.id.txtConnectState);
        control=(ImageView)findViewById(R.id.controlScreen);// mouse control screen
        
        RelativeLayout buttons=(RelativeLayout)findViewById(R.id.buttons); // mouse,keyboard 
        
        //mouse,keyboard buttons
        btnLeft=(ToggleButton)findViewById(R.id.btnLeftClick);
        btnRight=(ToggleButton)findViewById(R.id.btnRightClick);
        btnkeyb=(View) buttons.findViewById(R.id.btnKeyboard);
        
        control.setOnTouchListener(this);// imageView üzerindeki event
        btnLeft.setOnTouchListener(this);// sol click üzerindeki event
        btnRight.setOnTouchListener(this);// sağ click üzerindeki event
        btnkeyb.setOnTouchListener(this);// keyboard olayı
        btnkeyb.setOnKeyListener(mKeyListener); //keyboard onkey event   
        
        extras=getIntent().getExtras();// baglantıEkrani classındaki veriyi alır
        GelenIp=extras.getString(baglantiEkrani.IP); // gelen veri stringe aktarılır
        tvControl.setText(GelenIp); // ip ekrana yazılır 
             
        // yeni socket açılır
        try {
		this.socket = new DatagramSocket(8000);
		} catch(Throwable e) {
			e.printStackTrace();
		}
        
        keyboardEvent("connect");
       
     }
		 
	public void keyboardEvent(String state){
		
		try {
		    Controller.this.socket.close();
    	    this.socket = new DatagramSocket(8000);
        	byte[] message = null;
        	
        	message = (state).getBytes("UTF-8");
                        	
    		if (message != null) {
    			
    			this.packet = new DatagramPacket(message, message.length, InetAddress.getByName(GelenIp), 8000);
				((DatagramSocket) this.socket).send(packet);
				socket.close();
				Controller.this.socket=new DatagramSocket(8000);
    		}
		} catch (Throwable e) {
			e.printStackTrace();
		}
		
		
	}
	
    public void sendCharacter(String karakter){
	
		try {
		    
			Controller.this.socket.close();
    	    this.socket = new DatagramSocket(8000);
        	        	
        	byte[] message = (karakter+"son").getBytes("UTF-8");
                        	
    		if (message != null) {
    			
    			this.packet = new DatagramPacket(message, message.length, InetAddress.getByName(GelenIp), 8000);
				((DatagramSocket) this.socket).send(packet);
				socket.close();
				Controller.this.socket=new DatagramSocket(8000);
    		}
		} catch (Throwable e) {
			e.printStackTrace();
		}
		
				
	}
		
	// keyboard dan karakter girildiği zaman
	@SuppressLint("NewApi")
	OnKeyListener mKeyListener =new OnKeyListener() {
		
	
		@Override
		public boolean onKey(View v, int keyCode, KeyEvent event) {
		
			if(event.getAction()==KeyEvent.ACTION_MULTIPLE){
				keyboardEvent("keyboard");
				
				// keycode ları bilinmeyen karakterler
				if(keyCode == KeyEvent.KEYCODE_UNKNOWN) {
					String karakter = event.getCharacters();
					
					sendCharacter(karakter);
				
				} 
						
			}	
			 //keycode ları bilinen karakterler
			   if(event.getAction()==KeyEvent.ACTION_DOWN){
				keyboardEvent("keyboard");
				char press=(char)event.getUnicodeChar();
				String karakter = Character.toString(press);
				
				if(keyCode==KeyEvent.KEYCODE_DEL) karakter="backspace";
				if(keyCode==KeyEvent.KEYCODE_ENTER) karakter="enter";
			    if(keyCode==75){  if(karakter.equals("'")) karakter="tektirnak"; else karakter="cifttirnak";}
			    
				sendCharacter(karakter); 			
	
			   				
			}
			
			return false;
		}
	};
		
    @Override
    public void onDestroy() {
    	this.socket.close();
    	super.onDestroy();
    }
    
    // mouse eventları 
	@SuppressLint("NewApi")
	@Override
	public boolean onTouch(View v, MotionEvent event) {
		super.onTouchEvent(event);
		if(v==control) { // imageView ontouch event
    	switch(event.getAction()) {
	    	case MotionEvent.ACTION_DOWN: { // mouse touch event
	    		//mouse event bildirim için paket
	    		try {
				    Controller.this.socket.close();
            	    this.socket = new DatagramSocket(8000);
                	byte[] message = null;
                	
                	message = ("mouse").getBytes("UTF-8");
                                	
		    		if (message != null) {
		    			
		    			this.packet = new DatagramPacket(message, message.length, InetAddress.getByName(GelenIp), 8000);
						((DatagramSocket) this.socket).send(packet);
						socket.close();
						Controller.this.socket=new DatagramSocket(8000);
		    		}
				} catch (Throwable e) {
					e.printStackTrace();
	    		}
	    		
	    	    //mouse points
	    		final float x = event.getX();
	    		final float y = event.getY();
	    		// last position
	    		this.lastX = x;
	    		this.lastY = y;
	    		// first position
	    		this.firstX = x;
	    		this.firstY = y;
	    		// click time
	    		this.clickDown = new Date();
	    		break;
	    	}
	    	case MotionEvent.ACTION_MOVE: { //mouse move event
	    		// new position
	    		final float x = event.getX();
	    		final float y = event.getY();
	    		
	    		//  get delta
	    		final float deltax = x - this.lastX;
	    		final float deltay = y - this.lastY;
	    		// set last position
	    		this.lastX = x;
	    		this.lastY = y;
	    			    		
				try {
					
					
					byte[] message = (deltax + "," + deltay).getBytes("UTF-8");
					// package definition. ip of pc and port.
					this.packet = new DatagramPacket(message, message.length, 
							InetAddress.getByName(GelenIp), 8000);
					// send package
					this.socket.send(this.packet);
				} catch(Throwable e) {
					e.printStackTrace();
				}
	    		break;
	    	}
	    	
	    	case MotionEvent.ACTION_UP: { // mouse up olayı
	    		// get current position
	    		final float x = event.getX();
	    		final float y = event.getY();
	    		// get delta
	    		final float deltaX = this.firstX - x;
	    		final float deltaY = this.firstY - y;
	    		
	    		Date now = new Date();
	    		byte[] message = null;
	    		
	    		try {
	    			// when the users clicks and holds over 1 second send a double click to the pc.
		    		if (deltaX < this.tolerance[0] && 
	    				deltaX > this.tolerance[1] &&
	    				deltaY < this.tolerance[0] &&
	    				deltaY > this.tolerance[1] &&
	    				now.getTime() - this.clickDown.getTime() >= 1000) {
		    			
	    			// When the users clicks (not holding) send a normal click to the pc.
		    		} else if (x == this.firstX && y == this.firstY) {
		    			message = ("click").getBytes("UTF-8");
		    		}
		    		if (message != null) {
		    		
		    			this.packet = new DatagramPacket(message, message.length, InetAddress.getByName(GelenIp), 8000);
						this.socket.send(this.packet);	
		    		}
				} catch (Throwable e) {
					e.printStackTrace();
	    		}
	    		break;
	    	}
    	}
    
	}  
		// sol click olayı
		if(v==btnLeft){
		
			switch (event.getAction()) {
			
			case MotionEvent.ACTION_UP:
				
			
	    		byte[] message = null;
	    		
	    		try {
	    			
		    		message = ("click").getBytes("UTF-8");
	    		
		    		if (message != null) {
		    			
		    			this.packet = new DatagramPacket(message, message.length, InetAddress.getByName(GelenIp), 8000);
						this.socket.send(this.packet);	
		    		}
				} catch (Throwable e) {
					e.printStackTrace();
	    		}
				
				break;

		}
		
	}
		//sağ click olayı
	 	if(v==btnRight){
	       
	 		switch (event.getAction()) {
			
			case MotionEvent.ACTION_UP:
					
			byte[] message = null;
	    		
	    		try {
	    			
	    			message = ("r.click").getBytes("UTF-8");
	    		
		    		if (message != null) {
		    	
		    			this.packet = new DatagramPacket(message, message.length, InetAddress.getByName(GelenIp), 8000);
						this.socket.send(this.packet);	
		    		}
				} catch (Throwable e) {
					e.printStackTrace();
	    		}
				
				break;

			}
	 	
		}
	 	
	 	if(v==btnkeyb){
	 		
	 		// keyboard open
	 		InputMethodManager imm = (InputMethodManager) getSystemService(Context.INPUT_METHOD_SERVICE);
			imm.showSoftInput(btnkeyb, InputMethodManager.SHOW_FORCED);
			
          
  }// end-btnkeyboard
	 	
		return true;
  }//end-onTouch
	
}// end-mainActivity