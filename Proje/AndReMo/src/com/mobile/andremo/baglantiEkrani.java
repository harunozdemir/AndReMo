package com.mobile.andremo;

import org.xml.sax.Parser;
import android.R.bool;
import android.R.integer;
import android.R.layout;
import android.annotation.SuppressLint;
import android.app.Activity;
import android.app.AlertDialog;
import android.content.DialogInterface;
import android.content.Intent;
import android.os.AsyncTask;
import android.os.Bundle;
import android.text.Editable;
import android.text.Selection;
import android.text.TextWatcher;
import android.view.Menu;
import android.view.MenuItem;
import android.view.View;
import android.widget.Button;
import android.widget.EditText;
import android.widget.ImageView;
import android.widget.TextView;
import android.widget.Toast;


public class baglantiEkrani extends Activity {
     
	TextView txtIp;
	ImageView imgIp;
	Button baglan;
	EditText etGiris;
	
	public String ip="ip";
	public static String IP="IP";
	private String messsage="control";
 
	@Override
	protected void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		setContentView(R.layout.baglanti_ekrani);
		
		/*AdView adView = (AdView) this.findViewById(R.id.adView);
	    AdRequest adRequest = new AdRequest.Builder().build();
	    adView.loadAd(adRequest);*/

		baglan=(Button)findViewById(R.id.btnBaglan);
		etGiris=(EditText)findViewById(R.id.etIp);
		
		
	 baglan.setOnClickListener(new View.OnClickListener() {
			
			@Override
	 public void onClick(View v) {
				
				
		ip=etGiris.getText().toString();
		char [] control=ip.toCharArray();
		int cevirControl,controlopen=0,noktaSayisi=0,say=0;
		String karakter;
	
          for(int i=0;i<control.length;i++){ 
        	  if(control[i]=='.') {
        		  
        		  noktaSayisi++;
        	  if(i!=control.length-1) {
        		  if(control[i+1]=='.'){
        			  controlopen=1; 
        			  break;
        		  }
        	  }
        	  }
          }
          if(noktaSayisi!=3) controlopen=1;
          
          for(int i=0;i<control.length;i++){
       
        	   if(control[i]!='.') say++;
        	          	   
        	   if(say>3){ controlopen=1; break;}
        	   
        	   if(control[i]=='.') say=0;
          }
        	  
        for(int i=0;i<control.length;i++) {	  
        	 if(control[i]!='.') {
        		 
        		  try {
					 karakter=Character.toString(control[i]);
        		     cevirControl=Integer.parseInt(karakter); 
				} catch (Exception e) {
					controlopen=1;
					break;
					
				}        	 
       	  }
        	  
        	 
        }  	  
       int k=0,t=0;
     if(controlopen==0) {
    	 for(int i=0;i<control.length;i++){
     
        	if(control[i]=='.') k++;
        	
        	if(k==3) t=i;
        	
        }
    	
    	 try {
			 karakter=Character.toString(control[t]);
 		     cevirControl=Integer.parseInt(karakter); 
			} catch (Exception e) {
				controlopen=1;
							
			}  
    	 
    	 
     }	  
	if(controlopen==0){
		
		    Bundle extras=new Bundle();
			extras.putString(IP,ip); // bu değer IP adındaki key ile çağırılır
			Intent controlCagir=new Intent(getApplicationContext(),Controller.class);
			controlCagir.putExtras(extras); // veriyi intente ekler
			startActivity(controlCagir); 
	} 
	else openAlert(v);
		
			}
		});
	
  	}	
	
	 private void openAlert(View view) {
		
	   AlertDialog.Builder alertDialogBuilder = new AlertDialog.Builder(baglantiEkrani.this);
		
       alertDialogBuilder.setTitle(this.getTitle());
		
	   alertDialogBuilder.setMessage("IP adresiniz \n ___.___.___.___ formatında\n ve tamsayı olmalıdır !");
		
			
	  alertDialogBuilder.setPositiveButton("OK",new DialogInterface.OnClickListener() {

		@Override
		public void onClick(DialogInterface dialog, int which) {
			// TODO Auto-generated method stub
			
		}
		
	 		
	 });
		

	 AlertDialog alertDialog = alertDialogBuilder.create();
	
	 // show alert
		
	  alertDialog.show();
		
	   }

  
}
