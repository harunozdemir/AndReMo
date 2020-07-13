package com.mobile.andremo;

import android.app.Activity;
import android.content.Intent;
import android.content.res.Resources;
import android.os.Bundle;
import android.view.View;
import android.widget.ProgressBar;

public class hosgeldiniz extends Activity {
	
	protected ProgressBar bar;
	protected static final int TIMER_RUNTIME = 10000;
	protected boolean mbActive;

	@Override
	protected void onCreate(Bundle savedInstanceState) {
		// TODO Auto-generated method stub
		super.onCreate(savedInstanceState);
		setContentView(R.layout.hosgeldiniz);
		
  Thread yeni=new Thread() {
			
     public void run(){
        
        

        	  try {
					sleep(4000);
        	           	
			} catch (InterruptedException e) {
				// TODO Auto-generated catch block
				e.printStackTrace();
				
			} finally{
				
				Intent a=new Intent("android.intent.action.BAGLANTIEKRANI");
	        	startActivity(a);
				finish();
			
			}
     
    }//end-run

};//end-thread
		
	yeni.start();
		
		
	}


}
