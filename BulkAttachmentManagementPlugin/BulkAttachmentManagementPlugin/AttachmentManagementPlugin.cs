﻿using System.ComponentModel.Composition;
using XrmToolBox.Extensibility;
using XrmToolBox.Extensibility.Interfaces;

namespace BulkAttachmentManagementPlugin
{
    // Do not forget to update version number and author (company attribute) in AssemblyInfo.cs class
    // To generate Base64 string for Images below, you can use https://www.base64-image.de/
    [Export(typeof(IXrmToolBoxPlugin)),
    ExportMetadata("Name", "Bulk Attachment Manager"),
    ExportMetadata("Description", "Plugin to download attachments in bulk or individually.  You can also report on attachments in CRM."),
    // Please specify the base64 content of a 32x32 pixels image
    ExportMetadata("SmallImageBase64", "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"),
    // Please specify the base64 content of a 80x80 pixels image
    ExportMetadata("BigImageBase64", "iVBORw0KGgoAAAANSUhEUgAAAFAAAABQCAYAAACOEfKtAAAABmJLR0QA/wD/AP+gvaeTAAAACXBIWXMAAAycAAAMnAGTj5aaAAAAB3RJTUUH4QsQFCEXImafgQAAG1hJREFUeNrtnHlXW1e27X/qQEISGIFoRCNagQAbUC/s5I7UyB2VpOybqq/33nhfouKqxJW4wTFISCDAVgPYINRhOtEKkACd8/44WAEHJ7bjxHFu9hj8YYaHDmdqrT3XXHPtLRNFUeTP9dZL/icEfwL4Xpfyj/6CgiBQFIoIgoAoXNytZDIZcrkchUKBXC7/E8CXV7FY5ODggK3tLba3tzk8OIRiEUEmQyGXU15eTmVVFUajEb1ej0Kh+N8LoCiKyGSyC/8+OsqTTqWZCk0xOzPL80yak5MTQESlKsdgMGC1Wrl+4waWHgtqjQYZMpD98BkvIvUPD+CLlywWixweHrK9s8NKZoXFp095MjPLXDTK2toqQrGIKIoolUqqrlyhWDyloqKCQiFPg8lEVWUVmgoNKpXqJ4ErPfePVsasra3zdGGeubkYsXCMtfU1iqenKMvKUCkVKBQKRFFEKAqcnp4inL1+TU0NvdZe+geu0tbWRn19PWXqMiki/8gpfD51Dw4OePZ0ge9HHxKammQuOocog4GBAQYHr9Ha0kKFVosIHB0esra6RiwWw+/zkcvlWFxcZGdnl0KhQLm6HGOZEZn8Dw6gTCZDFEX2dvdYXHzGZCDIzPQ0z58/54rhCo0mE0O2Ya5du0ZLaysVFRUAHB0dsdawhrJMye7uLolEgr3dPR4/foxSpaLWWEtlVRUatfqPB+DLhLG3v8/M9DTBQBDf+DjLy3HqGur562efMTxso6nJRGVVFerycpQqVWmvrKmtpdFkYmDgKrFYjLGH3zM3P4dQLNLR0Y6psZEyo/En2Vn5oUbdi5Xbz/FsboHAxATToWnW19fQV1bS39/PJ3/5CxaLBfVZFJ0HXgQqtFqqq6tpb2+n1mgkk04zMzvDSiZNOplkbXWVcrUanV6P8hUgKj/kvW9jY4NIOEJwIsD4o0dsbG7S0trCsM2Gy+Oms7MTjUZzKfAy6RdwVkAbDAYaTSbq6uoonp6ytr7OUjyORqulvLwc5bnP+WCl3PlyYX9/j1g4zIO7d3n0/UOWlhaRyWUMDQ9z839u4fF60el0r/3ZCqUSY62Rzs4uamuN0r64vMzG+vpZ7fgHSGHZmTTb2dlmfm6OsbFxgsFJNjc3qG9spH9gAIfLSUdnZ2nzf51iGECpVFJbW0tbWxsymYyjwyPW1tbY3d3l9PQURFGK2A8NwJcJY3dnB9/YGGOPxvD7/CSTSVpazXxx82989PHHdHd3oy4vvzRtf2qplEquGKppam4id5AjlUqxt7fH0cEhglC8FLwPAsDzAOzv7/N04Sn+cT9Tk1Osr69TWVmJtc+Kx+ulr6+vVKa8DPzrPEepUKBSqVApFCCIFItFioLIT0mNDyKFBVEku7lJLBLF5/Ph8/tZf/6cpuZmnC4XnhEv3d3dJfDeqvEgCORyObKbWfb295ErFGg0FajLy5HL5NIGLPuAALygMHI5YtEYo6OjhKamyKTT6PV6HC4nX/7j73RbLOhfIow3iT6Ak+NjNjazLCcSbGxsolarqa2pQV+pQ6H8AOvAFwrj4OCApwtPmQpOMjs9zcb6OqbmJnp7e/F6vfT29qLX638xvx8fH7O5vs7ycpyjwyN6enpoam6i2mCQim/ZBwDgy/vWwcEBkxMTTEwE8Pt8xONxGk2N3Lx1C+/163R0dLxRqfLq50IhX2Alk2Hx2SLq8nJqjbV0dXfT0NhIeVnZhxGBJZUgiuT29nn69CkT/glmZ2fIbmUx1BjoG+jHe30Ea38fmnL1L35mPp9nK5tlbi5GZiVD8fQUvdFIk6mJ5uZmrlRVoVAqP6AUFmFjQyKMyWCA8fFxtre2MDWbGLY7cDiddHV1XwBPFMUL0fu6+58gCKysrPDg3l1CU9MkE0maWlro6+/H3N6OoaaG8g+qmSBKpUo0HGH03n1mZkLE43EMBgPDw3a+/Pvf6ejooKysDFEUKRQK5PN5isUiCqWSMpUKlUqFUqn8WRAFQSC7tUU0HOH27dvEojEaGhpxeTw4nU7MbeYLMvB3D6AgCOzu7LAwv4B/fJypqSAb6xs0NDQwcPUqDqezBB7A4eEhS/E4ieVlZEBDQwNGo5Fqg4GKioqf7KDkC9J+F4tE8E/4yaRXUMgVtJpbGRoaor+/H+PPdGHeO4AvE8b29ja+R2P4fD78Ph/pVApTUxM3v7zFyPWPsFi6SuC9KKqDgQDffnMHmVzGyMh1hm3DqMrKUJeXv/LlRVFke3ubh6MPuf3Pf5LJpKm6UoXdYcczMoLD6aClpYXy8vLX2gqUvwvCyO2zMDfP2KNxJieDrD5fRa+vpLfPyo0bH0mEodacNULz7O3tMBebIxaOsLS4hFanJZfLUTwtIiIivoJpj4+P2dnZJhIOE5qaJBqNIJcrGLLbGbl+HYfDgdlsvvBF/e5TeHNzk1gkwtjYOIEJH2vr6zS3NGN3OvGOjNDZ1VkCr1gskk6n8I2P8+TxEzIrK7SaWzGbzfT09mBqNlH5CntSEARWV9d4MHqfmckpFp89w1hXR2urGY/bjcNuf2Pw3guA51M3l8sRi0QYvfeAYDBIOp1Br9fhdLv58h9/p9dqRafTlsDb3NwkEg7zr69us7S0RFtbG9evX+fa0CAWSw/GOiPl5xoJ5xl6d2eHubko//rqK+Zjc9TUVGO327HZHbg97hJ4RUFALBaRKxTIZLKf7+K8L4Wxv7/Ps4UFghNBZqZDbGY3aTWbsVp7Gbk+Qk9PT0me5XI5kskk0UiUUEhqIqjVatra2hgcGqJ/oJ+amppL67Xjk2NWMivEIjGCwQCrK89RKpW0tbdjdzpwOJy0tbehUqrI5XJsbm4iCAJXrlxBp9OVGF32PrsxLxPG/v4+M6EQgYkA42OPiC/HaWho5PObf8PjlRoDL+SZIAisra7y4N497n13l43NTerq6ujp6cHldmHpsVBTW4NCcfmr7Gzv8nD0If/55htSqRRVVVUMDQ/jcrtwu9y0tbejVCo5PT1laXGR6VAIpVJJX38/bW1t6F+0898ngBc8jFyOZwsLBCYCTIem2NjYoKqqir6Bfj7+r4/p6elFo9GckUuO9fU1ZmdnCU2FiMZiVGg0dI9cxzMywsDAAHV19T8CTxQlbbu9vU00EmUqGCT85AnIZAzbbHhHvDgcDto7OlAoFBSLRVKpFNOhEMFAkMorVTQ0NtLS2oJMLn8leL9pCouiWCKM4ESQ8bFHbGxs0NTSgs1mw+l2SwpD8wNhZDIZvr1zh+lQiGQyRWNjA52dXdidDgYG+mk0NaIqU11KGOl0mgf37xMKhVh6tojBYKDVbMbpdmF3ODC3taFQKCgUCjwJh5mcCDDh95NKpejs6gSgrKzsZ2vB3wzAXG6/RBgz0yHiy3Gqqqqw2WzcvHWLjs5O1Go1IlA8PSWbzTIXi3Hvu7ssLCxQV1+Pw+nEZrMxNDSEyWSi7BLCEASB7W2pVLn91W3mYlGMxjqG7TYcDgcej4e2tjaUSiUnJyekkknufXcX39g46VSKsjIV5eXlVGgrfkRI7wXAFy+0MDd/5mEE2cxu0tDQSN9AP063uwQewHGhQCqZIhIOMzk5ydb2Flq9ji5LNw6Hg2uDQ5iam34EHkh1XjqdJhIO4/P5WEmnkcvkmM1mHA4HLpeL9vZ2FAoFR0dHJJNJ/GM+An4/y8tLaLU6enp7uXr1KsZaI3L5e1AiFwhDPPMwHo0x9kiq89LpDK1mM5/f/Bsf/9fHdHV1l8AD2NvdZezRI77+979ZW1vF1NREV1cXwzYb1wYHMTU1vXJaYHtnh9HRB/zrq9ukU2mqqw3YHU48Ix68Xm9pzzs5OSEWjfGfO3cIBgIszM+j1qhxOV189rfP6RsYoLamBqVS+dsDeB68/f19FuYX8Pl8TE4GWVtfR6/XYbX24vF6fyCMs8jb291lfn6ecDhMPB5HqVRisVhweTz09/VhampCpVJdShi7u7vEohFCU1PEojHkMhl2hwPvyAgut6u050nFeIZAQNrzEsvLaNRqui09OFzSFlFTW1sqt95LCkum9ybRcAT/+Dh+n4/V56s0tzTjdLsZuT5Cd/c5wjg9JZVMMfboEeFwmEw6hbnNjLnVjMPlor+/n8bGxh+B9+JZK8+f8/3Dh0xNBll6toTRaMRsNuPxenG5XbS1taFSqSgUCkQiUYJ+P+NjY6xkMuirKnHYHbjOyMVQU/NGbTHlr5G6ub19YpEoo/fuMzUVJJ1KoddXYndKHkZPTw96vR5RlJyvbDZLJBLm63//m3g8jrnNjNvjYWhwkB6rlbq6uh9JrBeEIUVelDvffEPkSVhqfdlsUnPA6y2Bd3p6Sjqd5t7duwT8fokwysvo6+vjsy8+Z2h4mOrq6jce9VW+y9QteRhPnzIZDDAzE2JjfQNTUxO9fVa8IyP0Wq0XFEYmk2EuFmMyOMna2ipKpRJzq5mhwUGuDg5SW1t76UsdHx+zurrKXGyO4ESAdDIFQKu5tUQYHWd7XqFQIJ1OM+GfYCY0TSa9glarxdJrwe3xcO3aNYxG4ysL/18NwFd6GP4JxsfHicfjNDQ0cPPLW9y48RGdXZ0lbSsIAuvra3x75w73vrvL1tYWpuYmLBYLDpeLHqv1leCJosjO9g6jD0a5ffsrVjMZqqqrpZkYlxO3x0PbGdsWi0UikQh3795lJhQiEV9Gp9Pi8Xr5y39/itVqpaa29q0dPeW7IAxBFDnI5Xi68JSJiQCzszNsb21hMBgYuHqVkesXW1K5XI611VVmZ2eZDoV4+nQBrVZHV1eXRBj9/dTV1V0KXqFQYGt7m8iTMFOTk8xHYyhUSgZtNtxeD85zRfLJyQnpdJrAxAQTPh+ZTAa9TkePtReny8nQ0BBVVVW/KPPeSQpnNzeJRWNMBSfx+3xkt7KShzFsx+F0YrF0XWhJJZNJ7t+7z/TUFKlkirq6erot3QzbbPT39dHY2HhpW0nyMDI8uD9KaHKSxWfPqDZU09bejtPtlvp5Z0Vy/uiIaDRKMBDg0cPvSSWSVOi0uL1eXC4XQ8PDVFZW/uJ3fysAz5v0+/tnhDE6yuz0tORh1BgYtjsueBjnW1LRSJS7333HfCxGY6MJh9OJ3eko1XmXse0LDyMSjnD7n/8kGolQW2fE4ZCMJpfbXdrzTk5OSCQSfPvNHfxn8kylUmHptvDpp58yeBZ5b2q+vzMAZec8jKcLT/H5fISmpthYX6fRJCmMlz2Mo6M86bSkMEKhKbLZLBUVFXR2dWKz2xgcGqLpFeAVCgVWVjJEwhH84z4y6TRyuaQw7HYpyl8ojEKhQCqRwD82TiAQYDm+jE6nO5uf8dDb13cBvDedoXlrAF9+0M7ONr6xMfzjfnx+P5l0GlNzEzdv3WLk+gidXd0X0nBvfwff+Dj/+uo262vr1Bnr6B65jt3hYGh4CFOT6YIiOf/cre1tHtwflTyMdBp9ZSVDNjserxu310t7m9SSEgWByJMwd7/7Dr/fz/zcHOVqNQ6Xk8+++Jxr165Re67Oe1PC+EUAlr4xQWQ/tyfN5z0ak6aknj9Hr9fT22tl5KMbWHutEhiiFD27uzvEYnM8efKEpaUl1OVqenp68Ix4GRgYwGRqvLQxcHx8zM72DpEnYUKTk0QjEeRyOUM2Ozc+uoHzLPKUSiWnxSLpRIKJwA8VgFqjwdJjwe12Y7PZqDUaS+XWu0jfN09hUWRjfZ1YNMrYmDSft76+TlNzMw6XE693hI6OjlIkCYJAJp1mfGyMx48fk8mkaGtro62tDZfbxcDVARobTZSVl73Cw1hl9MEoU2eEUVv3QmG4JfA6JPDy+TxzsRiBiQnGHo2RTmeorKzE6XBgdzlwuJxUGwxvbLy/EwBf9jCi4TD3791ncnKSZDIp/aEuJ1/+4x/0WntLsyrFYpGtrS1i0Sj/vn2bxaUlzOY2Rq5fZ2hoCEtPD3X1dZdqW1EU2d3dZS42x+3bXzEfjVFtqMbhcGC326W0PYu8YrFI+kVLanycZCqFSqWkv7+fL27dZGho8Eye/TrTzMrXSV1RFMnnj0gmkszMzjI7O8PGxgYt5las1j483hEsFgt6ndSGP8gdkE6nic3FmJkKsbG5iUatob2jXTKurw5IHsYlzcrj42NWnj8nFo0SnAiwmsmgUEkehsPplAij7YfISyeT+H1+pkMhVlZW0FdW0mOx4B0Z4dq1q2dF8rshjLcCUBAEcvv7LC/HmZ6eYWpqiuVEgtraWr74n5t4vSNYui2lmkoQBFbXVrl/7x737t1jK5ulvkGq8xwOJ5aenleCB7C7u8v3Dx9y55tvSCdTVFVXM3jWsXa53Rf2vBcN1+lQiMTyMhVaSWF88sknWPv7LoD3rlP3jQDczGaZmZll+swRq6yqoq+vj48/+lgaq9VqS7MqOzs7LC4+4/Hjx4SfPEGj0eAdGcHjHZEURn3dj8AreRg7O8SiEaYmg0SehAEYPlMYDoejVOeJgkA6kSAwMYFvfJyVlRUqtFosFokwhu02qqurL6la30MKF4tFVp8/J+D3E4lEMBhquH7jBi63m87OLiq0P2jbna0dHj+eYWZmhmw2S01NLa2tLVy9do3+PqkldVmT8oWHMTr6gNDUFEvPls48jFZcLucFeVYoFIg8CTMRkAgjmUqhr6zE4/XidrsZstlekme/Hng/C6AoihweHpLJZIhGo6TTGa5dG+Svf/0r/QMDJfAAhKJ0/GA6NEM4HKZYFOjv7y/91NfX/wi8lz2Mf30lTUkZjUaGbZKH4fZ4SvLs5OSEVCLB3e++Y3x8nHQ6g0qlpMdi4ZNPPmHYLoH3tqfP3ymAxWKRXC5HKpEkk85wdHCIRq2mqclEa5sZnf7iZKggCuQOcsTjcVZWVmg0NdA30F8ygC5zz172MNKpNHKZTFIYDjsul6vUVTk6OiKZSOAbG8fv9xOPx6k8O9LlHZEGLn9I21+HMN4IwHw+TyKRYGZmhnQqRW2tEbNei6mp6dJzY4IgcHJywu7uDkdHh1QbqrH2W+nutaDVaS99me3tbR7cv8/tr26zkn7hYTjweL14vN4L2jYWjfKfb+4QCASYn5tDo9HgdDj44tbNc2zLr0oYbwTgyfExa6urLD1bZGtri2pDNaYmEzWvMFtkZ3+0TCZDoZCOBYiCiCj8+Ch+IV9gZ3eXaCRCKBRiLhZFLpNjdzhLHkbbOQ8jlU4TDATw+/0sx5dRq9VYeizYXQ6GhgbPsa34q+95rwWgiMhpsUhuP0c2u0n+6AhDTQ0NDQ1UvWJmWH52iUN9XR3b29tsbm4yNRUCwOv1lgpmQRBYXo4zPjbO9PQ0S88WMRrrJIUx4vmRhxGORAhOTPDo4fdkUil0Oh1OlxO3243D5cRQU3MOtN8WvFcCKBNliILIcaHA0dERp8UiFRUVVF25gqaiAsUlm7RMLken09He2cn+fo7nq6tMBiY4OSnQ0txMe0cHcrmcjY1NHj9+wp2vv2Z+fp4aQw02u62Uupd5GBM+H8lEEqVKRV+flc+++BybzXYmz97vecnLI1AGAiLF4iknJyeIgoCqTIVarUalKrt0f5HL5egrK7FarZwcn0gEkUkTC0d5WPc9KysrADxfWyMSDrO+sU6Zqoy2zg5JYbicr/AwQmQyGbQ6LZZuCx6v5GG8aAz8loTx5nWgePYj7XDwE1cwyOVyqqurGbbb0Ffqkctl5AtHJBIJ/t//+b8UhSKiIKBUKqmsqqK5uYXOzk4Gh4YYHB6itbX1nIcR5d7du8yEpknEl9HrdLi9Xj799FN6+/reeUvqV0hhETmgUCpQlamQF/KcHB+Tz+c5Pj0p3XTxMuupVCoMBgOdXZ3s7e2xs7PN8fExi4uLbGWzCEWRqitVNDY2Yu3rw2a3S56vyYRS+YOHEfQHCPj9ZNIZdDotPdZeXC7XhU7y+4y6n4/AsyuRytVqNBUVHB4dcXh4wPbONgcHBwiC8JM9w8qqKrot3chkMlpaW8lkMuzt7iGKAlqtTrI5rb10dXXT0NiAUqkgf5QnEo0QDEiTW+lUCu2ZtnW6nAwND1/oJP8ewPvJFFYolOh0empqazk4yLG7u8vq6nP2dvekA8g/sVRKFY2NJqqrDVwbHOS4cEyxeIoIKORSVGsqNOi1euQKOScnJySTCb698x8m/H5WMhnKysuw9Fr4y39/ytDQEJWVlb8b0F4LQFWZCmN9PR0dHRzs7xObj1E4OSGb3aT4igg8H4nl5eWvNR6Wz+clhTHuIxgIkEgso6+spK+vD7fHg9VqvaBtfy+p+7MAlpeXY25tRRQEstksj8YekU6kWXO5pBPc517obVNKKBaJhCUPI+iXpqQ0ajUOu6PkYbwvhfG665VFlHS3VBWt5laampuo0GopFPKsZDKklhMc5HJS3f8ak+yXrePCMfGlOBMTE4yPjfPs2TPUag3dZ7PPQ8PD1Dc0XFpzfhAp/OLb1mq1tDQ3MzAwgFwmI51M8c3XX7OV3cI94n0rZ//w8JBwOCwpjO8fkUok0Oq0uFySurA7HBcaAx8sgBKZKGhobMTtdqNSKolFo9z99i6bG5sYG+rp7e0tHf7jkogURZHSpQNyOacnJywnEty/e5fxsTFSyVRpKv7zv33BsG0YQ03Nb9qS+nUBlMupqa3l6rVrPxhLkSjxeJzvRx+ytb1FjaEGtVqNurwcjUa6Ok5+dkva6ekphXyeQqEgzbVsbREJh3k8O8tWdoua2ho6u7rweLwMnHVVfg8K47Xt3te5/k4QBPKFPOmkNKgTCARIxOMcHh2h1mhobW2ho6OD5uZmjHV1VFZWolKppBskDw/Z2tri+coK8aU4yeVlstkshUKhNJrh8Xiw9PZe2nT94CNQFEXkcjkVmgpaWls4PjmW7iQtCgSDARLLy+xsZTnY32d7a4v6+nr0VVXSsalikYPcAdlslpVMmsVni6ysrKBUKmlpbaW/bwDvyAjDNtvvts57JxH4smuWTCRZmF/gyZNZkskkRweHUh9QqUCt1qBQKlDIFQiiSPH0lHyhwMnJCUKxSFlZGXV1dXR1d9PT04O1v4+GhoYPEry3AlAQBPL5PPv7+2xsrJNKpni6sMDis0XW19fJ5/NnYAnIZBIJlZWVUV0tmUQdnZ2YzWaaW5qpNhjQ6XRvdfnrB5PCLysAuVxORUUFGo2G2tpaamuMaLVaKioqWH2+Su4gRyFfQChK1yWpVCrp/xqNdLS309ndjanJRGVl5QWm/RAI451E4MvdruN8nr29Pfb298kf5Tk9lVL1hdhTIEOuVKA+u4dPr9OhPmPqP8L6xZfQvrAmBUEo1Xyi+MOpcdlZt1p21jOUyeVvrV7+kAD+b19/3qX/J4Dvd/1/oL1Rt3bZWlwAAAAASUVORK5CYII="),
    ExportMetadata("BackgroundColor", "Teal"),
    ExportMetadata("PrimaryFontColor", "Black"),
    ExportMetadata("SecondaryFontColor", "Gray")]
    public class AttachmentManagementPlugin : PluginBase
    {
        public override IXrmToolBoxPluginControl GetControl()
        {
            return new PluginControl();
        }
    }
}
