package com.example.mainactivity


import android.os.Bundle
import android.view.*
import androidx.fragment.app.DialogFragment
import androidx.recyclerview.widget.LinearLayoutManager
import androidx.recyclerview.widget.RecyclerView
import com.example.mainactivity.retrofit.INodeJS
import com.example.mainactivity.retrofit.RetrofitClient
import io.reactivex.android.schedulers.AndroidSchedulers
import io.reactivex.disposables.CompositeDisposable
import io.reactivex.schedulers.Schedulers
import kotlinx.android.synthetic.main.email_dialog.view.*
import kotlinx.android.synthetic.main.fragment_anvil.*
import retrofit2.Retrofit


class EmailDialog : DialogFragment() {

    lateinit var myAPI: INodeJS
    var compositeDisposable = CompositeDisposable()

    override fun onCreateView(
        inflater: LayoutInflater,
        container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View? {

        //iniciar API
        val retrofit: Retrofit = RetrofitClient.instance
        myAPI = retrofit.create(INodeJS::class.java)

        val rootView = inflater.inflate(R.layout.email_dialog, container, false)

        var emailList = mutableListOf(
            Items(
                "Ores",
                "You have: ${MailBox.Ores} to accept from your brother",
                R.drawable.ic_gold_ingot
            ),
            Items(
                "Ingot",
                "You have: ${MailBox.Bars} to accept from your brother",
                R.drawable.ic_ingot
            )

        )

        val MylistView = rootView.findViewById<RecyclerView>(R.id.emailRecycler)
        MylistView.adapter = EmailAdapter(emailList)
        MylistView.layoutManager = LinearLayoutManager(this.activity)

        rootView.closeEmail.setOnClickListener {
            dismiss()
        }

        rootView.acceptDelivery.setOnClickListener {
            retract()
        }
        return rootView
    }

    override fun onStart() {
        if (dialog != null) {
            dialog!!.window?.setLayout(
                ViewGroup.LayoutParams.MATCH_PARENT,
                ViewGroup.LayoutParams.MATCH_PARENT
            )
        }
        super.onStart()
    }

    fun retract(){
        compositeDisposable.add(myAPI.receiveDelivery(User.UserID, 1)
            .subscribeOn(Schedulers.io())
            .observeOn(AndroidSchedulers.mainThread())
            .subscribe {
                Resources.Nuggets += MailBox.Ores
                Resources.Bars += MailBox.Bars
                MailBox.Ores = 0
                MailBox.Bars = 0
                dismiss()
            }
        )
    }

}