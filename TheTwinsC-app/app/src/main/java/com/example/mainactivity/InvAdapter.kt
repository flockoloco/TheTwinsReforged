package com.example.mainactivity


import android.util.Log
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import androidx.appcompat.app.AlertDialog
import androidx.fragment.app.Fragment
import androidx.fragment.app.FragmentManager
import androidx.fragment.app.FragmentTransaction
import androidx.recyclerview.widget.RecyclerView
import com.example.mainactivity.retrofit.INodeJS
import com.example.mainactivity.retrofit.RetrofitClient
import io.reactivex.android.schedulers.AndroidSchedulers
import io.reactivex.disposables.CompositeDisposable
import io.reactivex.schedulers.Schedulers
import kotlinx.android.synthetic.main.deliverydialog.*
import kotlinx.android.synthetic.main.items_layout.view.*
import retrofit2.Retrofit

class InvAdapter(var itemInv: List<Items>) : RecyclerView.Adapter<InvAdapter.InvViewHolder>() {

    val bool:Boolean = false

    lateinit var myAPI: INodeJS
    var compositeDisposable = CompositeDisposable()

    inner class InvViewHolder(itemView: View) : RecyclerView.ViewHolder(itemView)

    override fun onCreateViewHolder(parent: ViewGroup, viewType: Int): InvViewHolder {
        val view = LayoutInflater.from(parent.context).inflate(R.layout.items_layout, parent, false)

        //iniciar API
        val retrofit: Retrofit = RetrofitClient.instance
        myAPI = retrofit.create(INodeJS::class.java)

        return InvViewHolder(view)
    }

    override fun getItemCount(): Int {
        return itemInv.size
    }

    override fun onBindViewHolder(holder: InvViewHolder, position: Int) {
        holder.itemView.apply {
            itemTitle.text = itemInv[position].name
            itemDescription.text = itemInv[position].details
            itemIcon.setImageResource(itemInv[position].icon)
        }
        holder.itemView.setOnClickListener { view ->
            val dialogBox = LayoutInflater.from(view.context).inflate(R.layout.deliverydialog, null)
            val builder = AlertDialog.Builder(view.context)
                .setView(dialogBox)
                .create()


            //Ores on click listener
            if (position == 1) {
                builder.show()
                builder.deliveryTxt.text = "How many ores do you want to send to your brother?"
                builder.deliveryImg.setImageResource(R.drawable.ic_gold_ingot)

                builder.deliveryClose.setOnClickListener {
                    builder.dismiss()
                }
                var count = 0
                builder.deliveryDecrease.setOnClickListener {
                    count--
                    if (count < 0) {
                        count = 0
                    }
                    builder.deliveryAmount.text = "$count"
                }

                builder.deliveryIncrease.setOnClickListener {
                    count++
                    if(count > Resources.Nuggets){
                        count = Resources.Nuggets
                    }
                    builder.deliveryAmount.text = "$count"
                }

                builder.deliverySend.setOnClickListener {
                    sendItemsDB(count, 0, 0)
                    Resources.Nuggets -= count
                    builder.dismiss()
                }
            }
            //Ingots on click listener
            if (position == 2) {
                builder.show()
                builder.deliveryTxt.text = "How many ingots do you want to send to your brother?"
                builder.deliveryImg.setImageResource(R.drawable.ic_ingot)

                builder.deliveryClose.setOnClickListener {
                    builder.dismiss()
                }
                var count = 0
                builder.deliveryDecrease.setOnClickListener {
                    count--
                    if (count < 0) {
                        count = 0
                    }
                    builder.deliveryAmount.text = "$count"
                }

                builder.deliveryIncrease.setOnClickListener {
                    count++
                    if(count > Resources.Bars){
                        count = Resources.Bars
                    }
                    builder.deliveryAmount.text = "$count"
                }

                builder.deliverySend.setOnClickListener {
                    sendItemsDB(0, count, 0)
                    Resources.Bars -= count
                    builder.dismiss()
                }
            }
        }
    }

    private fun sendItemsDB(Ores: Int, Ingots: Int, Type: Int) {
        compositeDisposable.add(myAPI.sendDelivery(
            User.UserID,
            Ingots,
            Ores,
            Type
        )
            .subscribeOn(Schedulers.io())
            .observeOn(AndroidSchedulers.mainThread())
            .subscribe {}
        )
    }
}