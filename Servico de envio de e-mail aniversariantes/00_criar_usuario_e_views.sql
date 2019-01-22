drop user sys_aniversariante cascade
/
create user sys_aniversariante identified by sys_aniversariante
/
grant connect to sys_aniversariante
/
drop view own_apdat.funcesp_vw_email_aniver
/         
create view own_apdat.funcesp_vw_email_aniver
as select con_cdicontratado
     , con_dssnome 
     ,CON_DTDNASCIMENTODATA
     , case when ccu_d1scentrocustores = 'QDPJ'  
            then 'PJ'
            when ccu_d1scentrocustores = 'QDPS'
            then 'RSS'
            else ccu_d1scentrocustores
             end as area
from own_apdat.contratados   contratado
    ,own_apdat.centroscustos ccusto
    ,own_apdat.situacoes situacao
where contratado.con_cdicentrocusto = ccusto.ccu_cdicentrocusto
 and contratado.CON_CDISITUACAO = situacao.SIT_CDISITUACAO
 and sit_d1ssituacaores in ('Ativo','Férias')
/          
          
grant select on own_apdat.funcesp_vw_email_aniver to sys_aniversariante
/
create synonym sys_aniversariante.funcesp_vw_email_aniver for  own_apdat.funcesp_vw_email_aniver
/
grant select on empregado to sys_aniversariante
/

/*
select NUM_RGTRO_EMPRG
     , nom_emprg
     , 'tiago.pinheiro@funcesp.com.br' cod_email_emprg 
     , area
from own_apdat.funcesp_vw_email_aniver vw_a
    ,empregado empregado    
where  empregado.cod_emprs = 4
 and empregado.num_rgtro_emprg = vw_a.con_cdicontratado
 and lower(empregado.cod_email_emprg) like '%funcesp.com.br'
 and case when con_cdicontratado = 2056
          then '14/09' \*  Ana Cláudia Alves Santos Francisco de 14/09/1971 para 23/09/1971 *\ 
          else to_char(vw_a.CON_DTDNASCIMENTODATA,'DD/MM') 
          end  = to_char(sysdate,'DD/MM') 
          
/*/

/* select NUM_RGTRO_EMPRG
     , nom_emprg
     , cod_email_emprg 
     ,   area
from  own_apdat.funcesp_vw_email_aniver vw_a
    ,empregado empregado    
where empregado.cod_emprs = 4
 and empregado.num_rgtro_emprg = vw_a.con_cdicontratado
 and lower(empregado.cod_email_emprg) like '%funcesp.com.br'
 and NUM_RGTRO_EMPRG in (1507,2191,2260,2207)
 /*/
